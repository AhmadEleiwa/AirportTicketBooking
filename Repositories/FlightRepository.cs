using System.Text.Json;
using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly string _path;
    public FlightRepository(string path)
    {
        this._path = path;
    }
    public List<Flight> GetAll()
    {
        if(!File.Exists(_path))return new List<Flight>();

        string json  = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<Flight>>(json) ?? new List<Flight>();
    }
    public void SaveAll(List<Flight> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented= true});
        File.WriteAllText(_path, json);
    }
}