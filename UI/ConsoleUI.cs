using AirportTicketBooking.Models;
using AirportTicketBooking.Services;

public class ConsoleUI
{
    enum State { InitMenuState, PassangerState, ManagerState }
    private State state = State.InitMenuState;

    private readonly FlightService _flightService;
    private readonly BookingService _bookingService;

    private string currentPassengerId = "P1"; // temp for testing

    public ConsoleUI(FlightService flightService, BookingService bookingService)
    {
        _flightService = flightService;
        _bookingService = bookingService;
    }

    public void Run()
    {
        while (true)
        {
            switch (state)
            {
                case State.InitMenuState:
                    InitMenu();
                    break;

                case State.PassangerState:
                    PassangerMenu();
                    break;

                case State.ManagerState:
                    ManagerMenu();
                    break;
            }
        }
    }

    public void InitMenu()
    {
        while (true)
        {
            Console.WriteLine("Select Role:");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");

            var input = Console.ReadLine();

            if (input == "1")
            {
                state = State.PassangerState;
                break;
            }
            else if (input == "2")
            {
                state = State.ManagerState;
                break;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }

    public void PassangerMenu()
    {
        while (true)
        {
            Console.WriteLine("\nPassenger Panel");
            Console.WriteLine("1. Search Flights");
            Console.WriteLine("2. Cancel Booking");
            Console.WriteLine("3. Modify Booking");
            Console.WriteLine("4. View My Bookings");
            Console.WriteLine("0. Back");

            var input = Console.ReadLine();

            if (input == "0")
            {
                state = State.InitMenuState;
                return;
            }

            switch (input)
            {
                case "1":
                    SearchFlights();
                    break;

                case "2":
                    Console.Write("Enter Booking Id: ");
                    var cancelId = Console.ReadLine();
                    _bookingService.Cancel(Guid.Parse(cancelId));
                    Console.WriteLine("Cancelled.");
                    break;

                case "3":
                    Console.Write("Enter Booking Id: ");
                    var bookingId = Console.ReadLine();

                    Console.WriteLine("Select Class: 0 Economy, 1 Business, 2 First");
                    var clsInput = Console.ReadLine();

                    var newClass = (FlightClass)int.Parse(clsInput);

                    _bookingService.ModifyClass(Guid.Parse(bookingId), newClass);
                    Console.WriteLine("Modified.");
                    break;

                case "4":
                    var bookings = _bookingService.GetPassengerBookings(Guid.Parse(currentPassengerId));

                    foreach (var b in bookings)
                    {
                        Console.WriteLine($"BookingId: {b.Id}, Flight: {b.FlightId}, Class: {b.FlightClass}, Price: {b.Price}");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    public void ManagerMenu()
    {
        while (true)
        {
            Console.WriteLine("\nManager Panel");
            Console.WriteLine("1. Search Flights");
            Console.WriteLine("2. View All Bookings");
            Console.WriteLine("3. Filter Bookings");
            Console.WriteLine("4. Import Flights (CSV)");
            Console.WriteLine("5. Show Validation Rules");
            Console.WriteLine("0. Back");

            var input = Console.ReadLine();

            if (input == "0")
            {
                state = State.InitMenuState;
                return;
            }

            switch (input)
            {
                case "1":
                    SearchFlights();
                    break;

                case "2":
                    var all = _bookingService.GetAll();

                    foreach (var b in all)
                    {
                        Console.WriteLine($"BookingId: {b.Id}, Passenger: {b.PassangerId}, Flight: {b.FlightId}, Class: {b.FlightClass}, Price: {b.Price}");
                    }
                    break;

                case "3":
                    FilterBookings();
                    break;

                case "4":
                    ImportFlights();
                    break;

                case "5":
                    ShowValidationMetadata();
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
    private void FilterBookings()
    {
        Console.Write("Passenger Id (or empty): ");
        var passengerId = Console.ReadLine();

        Console.Write("Flight Id (or empty): ");
        var flightId = Console.ReadLine();

        Console.Write("Class (0 Economy, 1 Business, 2 First, empty skip): ");
        var clsInput = Console.ReadLine();

        FlightClass? cls = null;
        if (!string.IsNullOrWhiteSpace(clsInput))
            cls = (FlightClass)int.Parse(clsInput);

        var result = _bookingService.GetAll()
            .Where(b =>
                (string.IsNullOrWhiteSpace(passengerId) || b.PassangerId.ToString() == passengerId) &&
                (string.IsNullOrWhiteSpace(flightId) || b.FlightId.ToString() == flightId) &&
                (cls == null || b.FlightClass == cls)
            ).ToList();

        foreach (var b in result)
        {
            Console.WriteLine($"BookingId: {b.Id}, Passenger: {b.PassangerId}, Flight: {b.FlightId}, Class: {b.FlightClass}");
        }
    }
    private void ImportFlights()
    {
        Console.Write("Enter CSV file path: ");
        var path = Console.ReadLine();

        var result = _flightService.ImportFromCsv(path);

        if (result.Errors.Any())
        {
            Console.WriteLine("\nErrors:");
            foreach (var e in result.Errors)
                Console.WriteLine(e);
        }

        if (result.ValidItems.Any())
        {
            _flightService.AddFlights(result.ValidItems);
            Console.WriteLine($"\nImported {result.ValidItems.Count} flights successfully.");
        }
    }
    private void ShowValidationMetadata()
    {
        var meta = FlightValidationMetadata.Get();

        foreach (var field in meta)
        {
            Console.WriteLine($"\n{field.Key}:");
            foreach (var rule in field.Value)
            {
                Console.WriteLine($" - {rule}");
            }
        }
    }
    private void SearchFlights()
    {
        var criteria = new FlightSearchCriteria();

        Console.Write("Departure Country (or empty): ");
        criteria.DepartureCountry = Console.ReadLine();

        Console.Write("Destination Country (or empty): ");
        criteria.DestinationCountry = Console.ReadLine();

        Console.Write("Max Price (or empty): ");
        var priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput))
            criteria.MaxPrice = decimal.Parse(priceInput);

        Console.WriteLine("Class: 0 Economy, 1 Business, 2 First (or empty)");
        var classInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(classInput))
            criteria.Class = (FlightClass)int.Parse(classInput);

        var flights = _flightService.Search(criteria);

        foreach (var f in flights)
        {
            Console.WriteLine($"Flight: {f.Id} | {f.DepartureCountry} -> {f.DestinationCountry} | Price: {f.GetPrice(criteria.Class)}");
        }

        // Passenger can book after search
        Console.WriteLine("Enter Flight Id to book or press Enter to skip:");
        var flightId = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(flightId))
        {
            var cls = criteria.Class ?? FlightClass.Economy;

            _bookingService.Book(Guid.Parse(currentPassengerId), Guid.Parse(flightId), cls);

            Console.WriteLine("Booked successfully.");
        }
    }

}