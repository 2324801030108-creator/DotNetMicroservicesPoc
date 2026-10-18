using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.Domain;

/// <summary>
/// An in-memory data store that serves the sample tariffs shipped with the service.
/// </summary>
public class InMemoryDataStore : IDataStore
{
    private readonly TariffLookup lookup;

    public InMemoryDataStore() : this(SampleTariffs.All())
    {
    }

    public InMemoryDataStore(IEnumerable<Tariff> tariffs)
    {
        if (tariffs == null) throw new ArgumentNullException(nameof(tariffs));
        lookup = new TariffLookup(tariffs);
    }

    public ITariffLookup Tariffs => lookup;

    private class TariffLookup : ITariffLookup
    {
        private readonly Dictionary<string, Tariff> tariffs;

        public TariffLookup(IEnumerable<Tariff> tariffs)
        {
            this.tariffs = tariffs.ToDictionary(t => t.Code, StringComparer.Ordinal);
        }

        public Task<Tariff?> this[string code] =>
            Task.FromResult(tariffs.TryGetValue(code, out var tariff) ? tariff : null);
    }
}
