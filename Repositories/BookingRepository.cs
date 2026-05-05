using System.Text.Json;
using AirportTicketBooking.Models;

namespace AirportTicketBooking.Repositories;

public class BookingRepository : IBookingtRepository
{
    private readonly string _path;
    public BookingRepository(string path)
    {
        this._path = path;
    }
    public List<Booking> GetAll()
    {
        if(!File.Exists(_path))return new List<Booking>();

        string json  = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<Booking>>(json) ?? new List<Booking>();
    }
    public void SaveAll(List<Booking> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented= true});
        File.WriteAllText(_path, json);
    }
}