using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public interface IFlightRepository
{
    List<Flight> GetAll();
    void SaveAll(List<Flight> flights);
    public Flight GetById(Guid id);
}