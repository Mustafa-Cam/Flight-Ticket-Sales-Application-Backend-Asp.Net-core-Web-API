using FlightBookingSystem.Data;
using FlightBookingSystem.Interfaces.IRepositories;
using FlightBookingSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Repositories
{
    public class UserRepository :Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        //public async Task<User> GetUserByIdAsync(int id)
        //{
        //    return await _context.Users.FindAsync(id);
        //}


        //public async Task AddUserAsync(User user)
        //{
        //    await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateUserAsync(User user)
        //{
        //    _context.Users.Update(user);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteUserAsync(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
