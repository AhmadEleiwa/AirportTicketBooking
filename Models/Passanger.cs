namespace AirportTicketBooking.Models
{

    public class Passanger
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}