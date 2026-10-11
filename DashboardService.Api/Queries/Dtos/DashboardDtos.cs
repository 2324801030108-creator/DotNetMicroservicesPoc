namespace DashboardService.Api.Queries.Dtos;

public class SalesTotalDto
{
    public decimal PremiumAmount { get; set; }
    public long PoliciesCount { get; set; }
}

public class PeriodSalesDto
{
    public string Period { get; set; } = string.Empty;
    public SalesTotalDto Sales { get; set; } = new();
}
