using FlightBookingSystem.Model;

namespace FlightBookingSystem.Interfaces.IRepositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
    }

}
