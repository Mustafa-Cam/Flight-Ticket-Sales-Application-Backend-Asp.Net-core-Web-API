    using FlightBookingSystem.Data;
    using FlightBookingSystem.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
    namespace FlightBookingSystem.Repositories
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            protected readonly ApplicationDbContext _context;
            protected readonly DbSet<T> _dbSet;

            public Repository(ApplicationDbContext context)
            {
                _context = context;
                _dbSet = _context.Set<T>();
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }


            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);

            }
        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task UpdateAsync(T entity)
            {
                _dbSet.Update(entity);

            }

            public async Task DeleteAsync(int id)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
        }

    }
