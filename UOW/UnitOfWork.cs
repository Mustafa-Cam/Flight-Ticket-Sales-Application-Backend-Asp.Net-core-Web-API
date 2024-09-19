// UnitOfWork.cs
using FlightBookingSystem.Data;
using FlightBookingSystem.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace FlightBookingSystem.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IFlightRepository Flights { get; private set; }
        public IUserRepository Users { get; private set; }

        public ITicketRepository Tickets { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IFlightRepository flightRepository, IUserRepository userRepository,ITicketRepository ticketRepository)
        {
            _context = context;
            Flights = flightRepository;
            Users = userRepository;
            Tickets = ticketRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
