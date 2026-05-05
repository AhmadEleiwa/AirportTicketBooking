using AirportTicketBooking.Models;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Services;

public class BookingService
{
    private readonly BookingRepository _bookingRepo;
    private readonly FlightRepository _flightRepo;
    public BookingService()
    {
        _bookingRepo = new BookingRepository("data/booking.json");
        _flightRepo = new FlightRepository("data/flight.json");
    }
    public void book(Guid passnagerId,Guid flightId, FlightClass cls )
    {
        var flight = _flightRepo.GetById(flightId);
        if(flight == null)
        {
           throw new Exception("Flight not found");
        }
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            PassangerId =passnagerId,
            FlightClass = cls,
            FlightId = flightId,
            BookingDate = DateTime.Now
        };
        var bookings = _bookingRepo.GetAll();
        bookings.Add(booking);

        _bookingRepo.SaveAll(bookings);
    }

}