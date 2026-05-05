using AirportTicketBooking.Models;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Services;


public class FlightService
{
    private IFlightRepository _flight_repo;
    private IBookingRepository _booking_repo;

    
    public FlightService()
    {
        _flight_repo = new FlightRepository("data/flight.json");
        _booking_repo = new BookingRepository("data/booking.json");
    }

    public List<Flight> Search(FlightSearchCriteria flightSearchCriteria)
    {
        var flights = _flight_repo.GetAll();
        var bookings = _booking_repo.GetAll();
        flights.Where(f =>
            (flightSearchCriteria.MaxPrice == null 
                || flightSearchCriteria.MaxPrice >= f.GetPrice(flightSearchCriteria.Class)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.DestinationCountry) 
                || f.DestinationCountry.Equals(flightSearchCriteria.DestinationCountry)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.ArrivalAirport) 
                || f.ArrivalAirport.Equals(flightSearchCriteria.ArrivalAirport)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.DepartureAirport) 
                || f.DepartureAirport.Equals(flightSearchCriteria.DepartureAirport)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.DepartureCountry) 
                || f.DepartureCountry.Equals(flightSearchCriteria.DepartureCountry)) &&
                
            (flightSearchCriteria.DepartureDate  == null 
                || flightSearchCriteria.DepartureDate.Value == f.DepartureDate.Date)

        );
        return flights;
    }
}