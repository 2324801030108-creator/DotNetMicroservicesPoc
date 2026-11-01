using System;
using System.Collections.Generic;
using DashboardService.Api.Queries.Dtos;
using MediatR;

namespace DashboardService.Api.Queries;

public class GetTotalSalesQuery : IRequest<GetTotalSalesResult>
{
    public string? ProductCode { get; set; }
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
}

public class GetTotalSalesResult
{
    public SalesDto Total { get; set; } = new();
    public Dictionary<string, SalesDto> PerProductTotal { get; set; } = new();
}

public class GetAgentsSalesQuery : IRequest<GetAgentsSalesResult>
{
    public string? AgentLogin { get; set; }
    public string? ProductCode { get; set; }
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
}

public class GetAgentsSalesResult
{
    public Dictionary<string, SalesDto> PerAgentTotal { get; set; } = new();
}

public class GetSalesTrendsQuery : IRequest<GetSalesTrendsResult>
{
    public string? ProductCode { get; set; }
    public DateTime SalesDateFrom { get; set; }
    public DateTime SalesDateTo { get; set; }
    public TimeUnit Unit { get; set; }
}

public class GetSalesTrendsResult
{
    public List<PeriodSaleDto> PeriodsSales { get; set; } = new();
}
