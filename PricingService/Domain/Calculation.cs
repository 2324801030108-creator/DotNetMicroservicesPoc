using System;
using System.Collections.Generic;

namespace PricingService.Domain;

/// <summary>
/// A single price calculation request: which product is being priced, over which
/// period, which covers were selected and what the customer answered.
/// </summary>
public class Calculation
{
    public Calculation(string productCode, DateTimeOffset policyFrom, DateTimeOffset policyTo,
        List<string> selectedCovers, Dictionary<string, object> answers)
    {
        ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
        PolicyFrom = policyFrom;
        PolicyTo = policyTo;
        SelectedCovers = selectedCovers ?? throw new ArgumentNullException(nameof(selectedCovers));
        Answers = answers ?? throw new ArgumentNullException(nameof(answers));
    }

    public string ProductCode { get; }
    public DateTimeOffset PolicyFrom { get; }
    public DateTimeOffset PolicyTo { get; }
    public IReadOnlyList<string> SelectedCovers { get; }
    public Dictionary<string, object> Answers { get; }
}
