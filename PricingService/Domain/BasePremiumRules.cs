using System;
using System.Collections.Generic;

namespace PricingService.Domain;

/// <summary>
/// A base premium rule calculates the price of a single cover with an optional
/// guard condition, e.g. cover "C1" costs <c>AREA * 1.25M</c> when <c>TYP == "APT"</c>.
/// </summary>
public class BasePremiumRule
{
    public BasePremiumRule(string coverCode, string? condition, string priceExpression)
    {
        CoverCode = coverCode ?? throw new ArgumentNullException(nameof(coverCode));
        Condition = condition;
        PriceExpression = priceExpression ?? throw new ArgumentNullException(nameof(priceExpression));
    }

    public string CoverCode { get; }
    public string? Condition { get; }
    public string PriceExpression { get; }
}

/// <summary>
/// The set of base premium rules of a <see cref="Tariff" />.
/// </summary>
public class BasePremiumRules
{
    private readonly List<BasePremiumRule> rules = new();

    public IReadOnlyList<BasePremiumRule> Rules => rules;

    public void AddBasePriceRule(string coverCode, string? condition, string priceExpression)
    {
        rules.Add(new BasePremiumRule(coverCode, condition, priceExpression));
    }

    /// <summary>
    /// Returns the price of the given cover by evaluating the first rule whose
    /// condition matches. Returns false (price 0) when no rule applies.
    /// </summary>
    public bool TryCalculatePrice(string coverCode, Dictionary<string, object> answers, out decimal price)
    {
        foreach (var rule in rules)
        {
            if (!string.Equals(rule.CoverCode, coverCode, StringComparison.Ordinal))
            {
                continue;
            }

            if (!RuleEvaluator.EvaluateCondition(rule.Condition, answers))
            {
                continue;
            }

            price = RuleEvaluator.EvaluateDecimal(rule.PriceExpression, answers);
            return true;
        }

        price = 0M;
        return false;
    }
}
