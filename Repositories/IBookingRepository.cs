using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public interface IBookingRepository
{
    public List<Booking> GetAll();
    public void SaveAll(List<Booking> Booking);
}