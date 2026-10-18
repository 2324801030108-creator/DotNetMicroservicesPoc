using System.Threading.Tasks;

namespace PricingService.Domain;

/// <summary>
/// Lookup of tariffs by product (tariff) code.
/// </summary>
public interface ITariffLookup
{
    /// <summary>Returns the tariff with the given code or null when it does not exist.</summary>
    Task<Tariff?> this[string code] { get; }
}

/// <summary>
/// Storage of the tariffs that can be priced by the service.
/// </summary>
public interface IDataStore
{
    ITariffLookup Tariffs { get; }
}
