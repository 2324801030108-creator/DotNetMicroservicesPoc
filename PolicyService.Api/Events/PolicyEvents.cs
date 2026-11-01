using MediatR;
using PolicyService.Api.Commands.Dtos;

namespace PolicyService.Api.Events;

/// <summary>
/// Raised by PolicyService after a policy has been created.
/// Consumed by PaymentService (create policy account), DashboardService (index sale)
/// and ChatService (notify agents).
/// </summary>
public class PolicyCreated : INotification
{
    public string PolicyNumber { get; set; } = string.Empty;

    public DateTime PolicyFrom { get; set; }

    public DateTime PolicyTo { get; set; }

    public PersonDto PolicyHolder { get; set; } = new();

    public string ProductCode { get; set; } = string.Empty;

    public decimal TotalPremium { get; set; }

    public string AgentLogin { get; set; } = string.Empty;
}

/// <summary>
/// Raised by PolicyService when a policy is terminated.
/// Consumed by PaymentService to close the policy account and return the remaining balance.
/// </summary>
public class PolicyTerminated : INotification
{
    public string PolicyNumber { get; set; } = string.Empty;

    public DateTime PolicyTo { get; set; }

    public decimal? AmountToReturn { get; set; }
}
