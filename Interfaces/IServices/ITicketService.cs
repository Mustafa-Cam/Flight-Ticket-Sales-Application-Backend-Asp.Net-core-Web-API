using FlightBookingSystem.DTO;
using FlightBookingSystem.Model;

namespace FlightBookingSystem.Interfaces.IServices
{
    public interface ITicketService
    {
        Task<bool> PurchaseTicketAsync(TicketDto ticketDto);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
    }

}
