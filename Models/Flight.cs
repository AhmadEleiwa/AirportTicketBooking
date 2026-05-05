namespace AirportTicketBooking.Models
{
    public class Flight
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal EconomyPrice { get; set; }
        public decimal BusinessPrice { get; set; }
        public decimal FirstClassPrice { get; set; }

        public decimal GetPrice(FlightClass ?flightClass)
        {
            switch (flightClass)
            {
                case FlightClass.Economy:
                    return EconomyPrice;
                case FlightClass.Business:
                    return BusinessPrice;
                case FlightClass.FirstClass:
                    return FirstClassPrice;

            }
            return EconomyPrice;
        }
    }
}