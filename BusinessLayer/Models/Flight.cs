namespace BusinessLayer.Models
{
    public class Flight
    {
        public Transport? Transport { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double Price { get; set; }

        public Flight(Transport transport, string origin, string destination, double price)
        {
            this.Transport = transport;
            this.Origin = origin;
            this.Destination = destination;
            this.Price = price;
        }
    }
}
