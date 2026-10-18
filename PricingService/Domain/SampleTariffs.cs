using System.Collections.Generic;

namespace PricingService.Domain;

/// <summary>
/// The tariffs the service can price: travel (TRI), house (HSI), farm (FAI) and car (CAR).
/// The rules mirror the tariffs used by the tests.
/// </summary>
public static class SampleTariffs
{
    public static IReadOnlyList<Tariff> All() => new List<Tariff>
    {
        Travel(),
        House(),
        Farm(),
        Car()
    };

    public static Tariff Travel()
    {
        var travel = new Tariff("TRI");
        travel.BasePremiumRules.AddBasePriceRule("C1", null,
            "(NUM_OF_ADULTS) * (DESTINATION == \"EUR\" ? 26.00M : 34.00M)");
        travel.BasePremiumRules.AddBasePriceRule("C2", null, "(NUM_OF_ADULTS + NUM_OF_CHILDREN) * 26.00M");
        travel.BasePremiumRules.AddBasePriceRule("C3", null, "(NUM_OF_ADULTS + NUM_OF_CHILDREN) * 10.00M");

        travel.DiscountMarkupRules.AddPercentMarkup("DESTINATION == \"WORLD\"", 1.5M);

        return travel;
    }

    public static Tariff House()
    {
        var house = new Tariff("HSI");

        house.BasePremiumRules.AddBasePriceRule("C1", "TYP == \"APT\"", "AREA * 1.25M");
        house.BasePremiumRules.AddBasePriceRule("C1", "TYP == \"HOUSE\"", "AREA * 1.50M");

        house.BasePremiumRules.AddBasePriceRule("C2", "TYP == \"APT\"", "AREA * 0.25M");
        house.BasePremiumRules.AddBasePriceRule("C2", "TYP == \"HOUSE\"", "AREA * 0.45M");

        house.BasePremiumRules.AddBasePriceRule("C3", null, "30M");
        house.BasePremiumRules.AddBasePriceRule("C4", null, "50M");

        house.DiscountMarkupRules.AddPercentMarkup("FLOOD == \"YES\"", 1.50M);
        house.DiscountMarkupRules.AddPercentMarkup("NUM_OF_CLAIM > 1 ", 1.25M);

        return house;
    }

    public static Tariff Farm()
    {
        var farm = new Tariff("FAI");

        farm.BasePremiumRules.AddBasePriceRule("C1", null, "10M");
        farm.BasePremiumRules.AddBasePriceRule("C2", null, "20M");
        farm.BasePremiumRules.AddBasePriceRule("C3", null, "30M");
        farm.BasePremiumRules.AddBasePriceRule("C4", null, "40M");

        farm.DiscountMarkupRules.AddPercentMarkup("FLOOD == \"YES\"", 1.50M);
        farm.DiscountMarkupRules.AddPercentMarkup("NUM_OF_CLAIM > 2", 2.00M);

        return farm;
    }

    public static Tariff Car()
    {
        var car = new Tariff("CAR");

        car.BasePremiumRules.AddBasePriceRule("C1", null, "100M");
        car.DiscountMarkupRules.AddPercentMarkup("NUM_OF_CLAIM > 2", 1.50M);

        return car;
    }
}
