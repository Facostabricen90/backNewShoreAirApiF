using BusinessLayer.Models.GateWays;
using BusinessLayer.Models;

namespace BusinessLayer.UseCases
{
    public class FlightUseCase
    {
        private readonly IFlights _flights;

        public FlightUseCase(IFlights flights)
        {
            _flights = flights;
        }

        public async Task<Journey> GetJourney(string origin, string destination)
        {
            List<Flight> allFlights = await _flights.GetFlights(0);

            List<Flight> route = FindRoute(allFlights, origin, destination);

            if (route.Count == 0)
            {
                throw new Exception("No direct route was found between the source and destination.");
            }
            double totalValue = CalculateTotalPrice(route);
            return new Journey(route, origin, destination, totalValue);
        }

        static double CalculateTotalPrice(List<Flight> flights)
        {
            return flights.Sum(flight => flight.Price);
        }

        static List<Flight> FindRoute(List<Flight> flights, string origin, string destination)
        {
            Queue<List<Flight>> queue = new Queue<List<Flight>>();
            HashSet<string> visited = new HashSet<string>();


            queue.Enqueue(new List<Flight>());

            while (queue.Count > 0)
            {
                List<Flight> currentPath = queue.Dequeue();

                if (currentPath.Count > 0)
                    visited.Add(currentPath.Last().Destination);


                if (currentPath.Count > 0 && currentPath.Last().Destination == destination)
                {
                    double totalPrice = CalculateTotalPrice(currentPath);
                    return currentPath;
                }

                List<Flight> nextFlights = flights
                    .Where(f => f.Origin == (currentPath.Count > 0 ? currentPath.Last().Destination : origin))
                    .ToList();

                foreach (Flight nextFlight in nextFlights)
                {
                    if (!visited.Contains(nextFlight.Destination))
                    {

                        visited.Add(nextFlight.Destination);

                        List<Flight> newPath = new List<Flight>(currentPath);
                        newPath.Add(nextFlight);

                        queue.Enqueue(newPath);
                    }
                }
            }
            return new List<Flight>();
        }
    }
}
