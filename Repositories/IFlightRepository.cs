using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public interface IFlightRepository
{
    public List<Flight> GetAll();
    public void SaveAll(List<Flight> flights);
    public Flight GetById(Guid id);
}