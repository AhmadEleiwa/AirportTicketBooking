using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public interface IBookingRepository
{
    List<Booking> GetAll();
    void SaveAll(List<Booking> Booking);
    
}