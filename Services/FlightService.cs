// FlightService.cs
using FlightBookingSystem.DTO;
using FlightBookingSystem.DTO.FlightBookingSystem.DTO;
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
            return await _unitOfWork.Flights.GetAllAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _unitOfWork.Flights.GetByIdAsync(id);
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _unitOfWork.Flights.AddAsync(flight);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            await _unitOfWork.Flights.UpdateAsync(flight);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteFlightAsync(int id)
        {
            await _unitOfWork.Flights.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchDto searchDto)
        {
            return await _unitOfWork.Flights.SearchFlightsAsync(searchDto);
        }

    }
}
