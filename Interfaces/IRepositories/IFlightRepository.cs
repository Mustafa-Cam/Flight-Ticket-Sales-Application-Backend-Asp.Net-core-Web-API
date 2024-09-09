using FlightBookingSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FlightBookingSystem.Interfaces.IRepositories
{

    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int id);
        Task AddFlightAsync(Flight flight);
        Task UpdateFlightAsync(Flight flight);
        Task DeleteFlightAsync(int id);
    }
}

