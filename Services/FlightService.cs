// FlightService.cs
using FlightBookingSystem.Model;
using FlightBookingSystem.Repositories;
using FlightBookingSystem.Services.FlightBookingSystem.Services;
using FlightBookingSystem.UOW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookingSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlightService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            return await _unitOfWork.Flights.GetAllFlightsAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _unitOfWork.Flights.GetFlightByIdAsync(id);
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _unitOfWork.Flights.AddFlightAsync(flight);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            await _unitOfWork.Flights.UpdateFlightAsync(flight);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteFlightAsync(int id)
        {
            await _unitOfWork.Flights.DeleteFlightAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
