public static class FlightValidationMetadata
{
    public static Dictionary<string, List<string>> Get()
    {
        return new Dictionary<string, List<string>>
        {
            { "Id", new List<string> { "Required", "Unique", "String" } },
            { "DepartureCountry", new List<string> { "Required", "Free Text" } },
            { "DestinationCountry", new List<string> { "Required", "Free Text" } },
            { "DepartureAirport", new List<string> { "Required", "Free Text" } },
            { "ArrivalAirport", new List<string> { "Required", "Free Text" } },
            { "DepartureDate", new List<string> { "Required", "DateTime", "Today → Future" } },
            { "EconomyPrice", new List<string> { "Required", "Decimal", "> 0" } },
            { "BusinessPrice", new List<string> { "Required", "Decimal", "> 0" } },
            { "FirstClassPrice", new List<string> { "Required", "Decimal", "> 0" } }
        };
    }
}