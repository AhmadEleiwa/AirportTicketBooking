namespace AirportTicketBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int PassangerId { get; set; }

        public FlightClass FlightClass { get; set; }

        public decimal Price { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}