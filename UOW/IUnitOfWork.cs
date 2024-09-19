// IUnitOfWork.cs
using FlightBookingSystem.Interfaces.IRepositories;
using System.Threading.Tasks;

namespace FlightBookingSystem.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IFlightRepository Flights { get; }
        IUserRepository Users { get; }
        ITicketRepository Tickets { get; }
        Task<int> CompleteAsync();
    }
}   
