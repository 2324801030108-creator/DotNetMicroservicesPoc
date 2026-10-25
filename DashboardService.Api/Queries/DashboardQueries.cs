using System;
using System.Collections.Generic;
using DashboardService.Api.Queries.Dtos;
using MediatR;

namespace DashboardService.Api.Queries;

public enum TimeUnit { Day, Month, Year }

public class GetTotalSalesQuery : IRequest<GetTotalSalesResult>
{
    public string? ProductCode { get; set; }
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
}

public class GetTotalSalesResult
{
    public SalesTotalDto Total { get; set; } = new();
    public Dictionary<string, SalesTotalDto> PerProductTotal { get; set; } = new();
}

public class GetAgentsSalesQuery : IRequest<GetAgentsSalesResult>
{
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
}

public class GetAgentsSalesResult
{
    public Dictionary<string, SalesTotalDto> PerAgentTotal { get; set; } = new();
}

public class GetSalesTrendsQuery : IRequest<GetSalesTrendsResult>
{
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
    public TimeUnit Unit { get; set; }
}

public class GetSalesTrendsResult
{
    public List<PeriodSalesDto> PeriodsSales { get; set; } = new();
}
