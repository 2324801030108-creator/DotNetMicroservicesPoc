using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PricingService.Api.Commands;
using PricingService.Api.Commands.Dto;
using PricingService.Domain;

namespace PricingService.Commands;

/// <summary>
/// Handles <see cref="CalculatePriceCommand" /> by validating it, looking the
/// tariff up in the data store and calculating the premium per cover.
/// </summary>
public class CalculatePriceHandler : IRequestHandler<CalculatePriceCommand, CalculatePriceResult>
{
    private readonly CalculatePriceCommandValidator validator = new();
    private readonly IDataStore dataStore;

    public CalculatePriceHandler(IDataStore dataStore)
    {
        this.dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
    }

    public async Task<CalculatePriceResult> Handle(CalculatePriceCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var tariff = await dataStore.Tariffs[request.ProductCode];
        if (tariff == null)
        {
            return CalculatePriceResult.Empty();
        }

        var answers = request.Answers.ToDictionary(a => a.QuestionCode, a => a.GetAnswer());

        var calculation = new Calculation(
            request.ProductCode,
            request.PolicyFrom,
            request.PolicyTo,
            request.SelectedCovers,
            answers);

        var premium = tariff.CalculatePrice(calculation);

        return new CalculatePriceResult
        {
            TotalPrice = premium.TotalPremium,
            CoverPrices = premium.Covers.ToDictionary(c => c.Key, c => c.Value.Price)
        };
    }
}
