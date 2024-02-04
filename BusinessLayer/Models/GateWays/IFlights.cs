using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Models.GateWays
{
    public interface IFlights
    {
        Task<List<Flight>> GetFlights(int id);
    }
}
