using System;
using System.Collections.Generic;

namespace PricingService.Domain;

/// <summary>
/// A tariff (insurance product) that knows how to calculate the premium of a
/// <see cref="Calculation" /> from its base premium rules and its
/// markup/discount rules.
/// </summary>
public class Tariff
{
    public Tariff(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }

    public string Code { get; }

    public BasePremiumRules BasePremiumRules { get; } = new();

    public DiscountMarkupRules DiscountMarkupRules { get; } = new();

    /// <summary>
    /// Calculates the premium for every selected cover. The first matching base
    /// premium rule of a cover determines its base price; all matching
    /// markup/discount rules are then applied as a factor on that price.
    /// </summary>
    public PremiumCalculationResult CalculatePrice(Calculation calculation)
    {
        if (calculation == null) throw new ArgumentNullException(nameof(calculation));

        var result = new PremiumCalculationResult();
        var answers = calculation.Answers;

        DiscountMarkupRules.TryCalculateFactor(answers, out var factor);

        foreach (var coverCode in calculation.SelectedCovers)
        {
            if (!BasePremiumRules.TryCalculatePrice(coverCode, answers, out var basePrice))
            {
                continue;
            }

            var price = basePrice * factor;
            result.Covers[coverCode] = new CoverPrice(coverCode, price);
            result.TotalPremium += price;
        }

        return result;
    }
}
