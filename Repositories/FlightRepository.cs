using FlightBookingSystem.Data;
using FlightBookingSystem.Interfaces.IRepositories;
using FlightBookingSystem.Model;
using Microsoft.EntityFrameworkCore;


namespace FlightBookingSystem.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }
    }
}
