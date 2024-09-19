using FlightBookingSystem.Data;
using FlightBookingSystem.Interfaces.IRepositories;
using FlightBookingSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Repositories
{
    public class TicketRepository : Repository<Ticket>,ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _context.Tickets
                .Include(t => t.Flight) // Flight bilgilerini de getiriyoruz
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }

}
