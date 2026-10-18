using System.Collections.Generic;

namespace PricingService.Domain;

/// <summary>Kết quả tính tổng phí bảo hiểm của một tariff.</summary>
public class PremiumCalculationResult
{
    public decimal TotalPremium { get; set; }

    /// <summary>Phí bảo hiểm chi tiết theo từng cover (key = cover code).</summary>
    public Dictionary<string, CoverPrice> Covers { get; set; } = new Dictionary<string, CoverPrice>();
}

/// <summary>Phí bảo hiểm của một cover riêng lẻ.</summary>
public class CoverPrice
{
    public CoverPrice()
    {
    }

    public CoverPrice(string coverCode, decimal price)
    {
        CoverCode = coverCode;
        Price = price;
    }

    public string CoverCode { get; set; }

    public decimal Price { get; set; }
}
