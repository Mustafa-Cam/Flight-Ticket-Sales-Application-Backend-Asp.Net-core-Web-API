using FlightBookingSystem.Model;
using System.Threading.Tasks;

namespace FlightBookingSystem.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
