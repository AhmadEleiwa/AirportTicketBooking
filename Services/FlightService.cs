using AirportTicketBooking.Models;
using AirportTicketBooking.Repositories;

namespace AirportTicketBooking.Services;


public class FlightService
{
    private IFlightRepository _flight_repo;
    private IBookingRepository _booking_repo;


    public FlightService()
    {
        _flight_repo = new FlightRepository("data/flight.json");
        _booking_repo = new BookingRepository("data/booking.json");
    }

    public List<Flight> Search(FlightSearchCriteria flightSearchCriteria)
    {
        var flights = _flight_repo.GetAll();
        var bookings = _booking_repo.GetAll();
        flights.Where(f =>
            (flightSearchCriteria.MaxPrice == null
                || flightSearchCriteria.MaxPrice >= f.GetPrice(flightSearchCriteria.Class)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.DestinationCountry)
                || f.DestinationCountry.Equals(flightSearchCriteria.DestinationCountry)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.ArrivalAirport)
                || f.ArrivalAirport.Equals(flightSearchCriteria.ArrivalAirport)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.DepartureAirport)
                || f.DepartureAirport.Equals(flightSearchCriteria.DepartureAirport)) &&

            (string.IsNullOrWhiteSpace(flightSearchCriteria.DepartureCountry)
                || f.DepartureCountry.Equals(flightSearchCriteria.DepartureCountry)) &&

            (flightSearchCriteria.DepartureDate == null
                || flightSearchCriteria.DepartureDate.Value == f.DepartureDate.Date)

        );
        return flights;
    }
    public ImportResult<Flight> ImportFromCsv(string path)
    {
        var result = new ImportResult<Flight>();

        if (!File.Exists(path))
        {
            result.Errors.Add("File not found");
            return result;
        }

        var lines = File.ReadAllLines(path).Skip(1); // skip header
        int row = 2;

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            try
            {
                var flight = new Flight
                {
                    Id = Guid.Parse(parts[0]),
                    DepartureCountry = parts[1],
                    DestinationCountry = parts[2],
                    DepartureAirport = parts[3],
                    ArrivalAirport = parts[4],
                    DepartureDate = DateTime.Parse(parts[5]),
                    EconomyPrice = decimal.Parse(parts[6]),
                    BusinessPrice = decimal.Parse(parts[7]),
                    FirstClassPrice = decimal.Parse(parts[8])
                };

                var errors = FlightValidator.Validate(flight);

                if (errors.Any())
                {
                    result.Errors.Add($"Row {row}: {string.Join(" | ", errors)}");
                }
                else
                {
                    result.ValidItems.Add(flight);
                }
            }
            catch
            {
                result.Errors.Add($"Row {row}: Invalid format");
            }

            row++;
        }

        return result;
    }

    public void AddFlights(List<Flight> flights)
    {
        var existing = _flight_repo.GetAll();
        existing.AddRange(flights);
        _flight_repo.SaveAll(existing);
    }
}