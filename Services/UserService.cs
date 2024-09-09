using FlightBookingSystem.Model;
using FlightBookingSystem.Repositories;
using FlightBookingSystem.Services.FlightBookingSystem.Services;
using FlightBookingSystem.UOW;
using System.Threading.Tasks;

namespace FlightBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetUserByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.Users.GetUserByEmailAsync(email);
        }

        public async Task AddUserAsync(User user)
        {
            await _unitOfWork.Users.AddUserAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _unitOfWork.Users.UpdateUserAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.Users.DeleteUserAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}