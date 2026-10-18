using System;
using System.Collections.Generic;

namespace PricingService.Domain;

/// <summary>
/// A percent markup/discount rule: when the condition matches, the price of every
/// cover is multiplied by <see cref="Multiplier" /> (e.g. 1.5M = +50%, 0.9M = -10%).
/// </summary>
public class DiscountMarkupRule
{
    public DiscountMarkupRule(string? condition, decimal multiplier)
    {
        Condition = condition;
        Multiplier = multiplier;
    }

    public string? Condition { get; }
    public decimal Multiplier { get; }
}

/// <summary>
/// The set of markup/discount rules of a <see cref="Tariff" />.
/// </summary>
public class DiscountMarkupRules
{
    private readonly List<DiscountMarkupRule> rules = new();

    public IReadOnlyList<DiscountMarkupRule> Rules => rules;

    public void AddPercentMarkup(string? condition, decimal multiplier)
    {
        rules.Add(new DiscountMarkupRule(condition, multiplier));
    }

    /// <summary>
    /// Returns the factor that has to be applied to a cover price (product of all
    /// matching rules). Returns false when no markup/discount applies.
    /// </summary>
    public bool TryCalculateFactor(Dictionary<string, object> answers, out decimal factor)
    {
        var matched = false;
        factor = 1M;

        foreach (var rule in rules)
        {
            if (!RuleEvaluator.EvaluateCondition(rule.Condition, answers))
            {
                continue;
            }

            factor *= rule.Multiplier;
            matched = true;
        }

        return matched;
    }
}
