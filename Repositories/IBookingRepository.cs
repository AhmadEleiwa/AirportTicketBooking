using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public interface IBookingtRepository
{
    List<Booking> GetAll();
    void SaveAll(List<Booking> Booking);
}