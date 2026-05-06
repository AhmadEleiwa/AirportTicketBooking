
using AirportTicketBooking.Services;

// create services (inject repos)
var flightService = new FlightService();
var bookingService = new BookingService();

// create UI
var ui = new ConsoleUI(flightService, bookingService);

// run app
ui.Run();