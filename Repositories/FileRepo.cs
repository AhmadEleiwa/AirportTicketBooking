using System.Text.Json;

namespace AirportTicketBooking.Repositories;

public class FileRepo<T>
{
    private readonly string _path;
    public FileRepo(string path)
    {
        this._path = path;
    }
    public List<T> GetAll()
    {
        if(!File.Exists(_path))return new List<T>();

        string json  = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
    public void Save(List<T> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented= true});
        File.WriteAllText(_path, json);
    }
}