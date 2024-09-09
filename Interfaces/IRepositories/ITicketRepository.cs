using FlightBookingSystem.Model;

namespace FlightBookingSystem.Interfaces.IRepositories
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
    }

}
