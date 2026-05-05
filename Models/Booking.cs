namespace AirportTicketBooking.Models
{
    public class Booking
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public Guid PassangerId { get; set; }
        public Guid FlightId { get; set; }

        public FlightClass FlightClass { get; set; }

        public decimal Price { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}