using AirportTicketBooking.Models;

public static class FlightValidator
{
    public static List<string> Validate(Flight f)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(f.Id.ToString()))
            errors.Add("Id is required");

        if (string.IsNullOrWhiteSpace(f.DepartureCountry))
            errors.Add("Departure Country is required");

        if (string.IsNullOrWhiteSpace(f.DestinationCountry))
            errors.Add("Destination Country is required");

        if (string.IsNullOrWhiteSpace(f.DepartureAirport))
            errors.Add("Departure Airport is required");

        if (string.IsNullOrWhiteSpace(f.ArrivalAirport))
            errors.Add("Arrival Airport is required");

        if (f.DepartureDate < DateTime.Now.Date)
            errors.Add("Departure Date must be today or future");

        if (f.EconomyPrice <= 0)
            errors.Add("Economy Price must be > 0");

        if (f.BusinessPrice <= 0)
            errors.Add("Business Price must be > 0");

        if (f.FirstClassPrice <= 0)
            errors.Add("First Class Price must be > 0");

        return errors;
    }
}