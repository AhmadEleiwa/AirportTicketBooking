namespace AirportTicketBooking.Models
{
    public class Booking
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public int PassangerId { get; set; }

        public FlightClass FlightClass { get; set; }

        public decimal Price { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}