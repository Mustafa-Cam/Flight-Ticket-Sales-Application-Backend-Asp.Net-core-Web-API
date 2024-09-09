﻿namespace FlightBookingSystem.Services
{

    using global::FlightBookingSystem.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace FlightBookingSystem.Services
    {
        public interface IFlightService
        {
            Task<IEnumerable<Flight>> GetFlightsAsync();
            Task<Flight> GetFlightByIdAsync(int id);
            Task AddFlightAsync(Flight flight);
            Task UpdateFlightAsync(Flight flight);
            Task DeleteFlightAsync(int id);
        }
    }
}