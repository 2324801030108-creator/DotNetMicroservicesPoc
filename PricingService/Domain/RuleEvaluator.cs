using System;
using System.Collections.Generic;
using DynamicExpresso;

namespace PricingService.Domain;

/// <summary>
/// Evaluates the string based pricing rules (conditions and price expressions)
/// such as <c>DESTINATION == "EUR" ? 26.00M : 34.00M</c> against the answers
/// given by the customer.
/// </summary>
public static class RuleEvaluator
{
    private static readonly Interpreter Expressions = new();

    public static bool EvaluateCondition(string? condition, Dictionary<string, object> answers)
    {
        if (string.IsNullOrWhiteSpace(condition))
        {
            return true;
        }

        return Convert.ToBoolean(Evaluate(condition, answers));
    }

    public static decimal EvaluateDecimal(string expression, Dictionary<string, object> answers)
    {
        return Convert.ToDecimal(Evaluate(expression, answers));
    }

    private static object Evaluate(string expression, Dictionary<string, object> answers)
    {
        var parameters = new Parameter[answers.Count];
        var index = 0;
        foreach (var answer in answers)
        {
            parameters[index++] = new Parameter(answer.Key, answer.Value);
        }

        return Expressions.Eval(expression, parameters);
    }
}
