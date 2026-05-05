namespace AirportTicketBooking.Models;


public class FlightSearchCriteria
{
    public decimal? MaxPrice;
    public string? DepartureCountry { get; set; }
    public string? DestinationCountry { get; set; }
    public string? DepartureAirport { get; set; }
    public string? ArrivalAirport { get; set; }
    public DateTime? DepartureDate { get; set; }
    public FlightClass? Class { get; set; }
}