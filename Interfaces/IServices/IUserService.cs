namespace FlightBookingSystem.Services
{

    using global::FlightBookingSystem.Model;
    using System.Threading.Tasks;

    namespace FlightBookingSystem.Services
    {
        public interface IUserService
        {
            Task<User> GetUserByIdAsync(int id);
            Task<User> GetUserByEmailAsync(string email);
            Task AddUserAsync(User user);
            Task UpdateUserAsync(User user);
            Task DeleteUserAsync(int id);
        }
    }
}
