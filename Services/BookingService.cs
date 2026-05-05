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
    public void book(Guid passnagerId, Guid flightId, FlightClass cls)
    {
        var flight = _flightRepo.GetById(flightId);
        if (flight == null)
        {
            throw new Exception("Flight not found");
        }
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            PassangerId = passnagerId,
            FlightClass = cls,
            FlightId = flightId,
            BookingDate = DateTime.Now
        };
        var bookings = _bookingRepo.GetAll();
        bookings.Add(booking);

        _bookingRepo.SaveAll(bookings);
    }

    public void Cancel(Guid bookingId)
    {
        var bookings = _bookingRepo.GetAll();
        bookings.RemoveAll(b => b.Id == bookingId);

        _bookingRepo.SaveAll(bookings);
    }
    public void ModifyClass(Guid bookingId, FlightClass newClass)
    {
        var bookings = _bookingRepo.GetAll();

        var booking = bookings.FirstOrDefault(b => b.Id == bookingId);

        if (booking == null)
            throw new Exception("Booking not found");

        var flight = _flightRepo.GetById(booking.FlightId);

        booking.FlightClass = newClass;
        booking.Price = flight.GetPrice(newClass);

        _bookingRepo.SaveAll(bookings);
    }

    // VIEW passenger bookings
    public List<Booking> GetPassengerBookings(Guid passengerId)
    {
        return _bookingRepo.GetAll()
            .Where(b => b.PassangerId == passengerId)
            .ToList();
    }

}