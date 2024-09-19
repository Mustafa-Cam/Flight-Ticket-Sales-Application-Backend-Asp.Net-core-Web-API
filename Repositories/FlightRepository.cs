using FlightBookingSystem.Data;
using FlightBookingSystem.DTO;
using FlightBookingSystem.DTO.FlightBookingSystem.DTO;
using FlightBookingSystem.Interfaces.IRepositories;
using FlightBookingSystem.Model;
using Microsoft.EntityFrameworkCore;


namespace FlightBookingSystem.Repositories
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        //Repository pattern olmadan
        //public async Task<IEnumerable<Flight>> GetAllAsync()
        //{
        //    return await _context.Flights.ToListAsync();
        //}

        //public async Task<Flight> GetByIdAsync(int id)
        //{
        //    return await _context.Flights.FindAsync(id);
        //}

        //public async Task AddAsync(Flight flight)
        //{
        //    await _context.Flights.AddAsync(flight);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateAsync(Flight flight)
        //{
        //    _context.Flights.Update(flight);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var flight = await _context.Flights.FindAsync(id);
        //    if (flight != null)
        //    {
        //        _context.Flights.Remove(flight);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchDto searchDto)
        {
            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(searchDto.Departure))
            {
                query = query.Where(f => f.Departure.Contains(searchDto.Departure));
            }

            if (!string.IsNullOrEmpty(searchDto.Arrival))
            {
                query = query.Where(f => f.Arrival.Contains(searchDto.Arrival));
            }

            // Tarih aralığı kontrolü
            if (searchDto.StartDate.HasValue && searchDto.EndDate.HasValue)
            {
                // StartDate ile EndDate arasındaki uçuşları filtrele
                query = query.Where(f => f.Date.Date >= searchDto.StartDate.Value.Date &&
                                         f.Date.Date <= searchDto.EndDate.Value.Date);
            }
            else if (searchDto.StartDate.HasValue)
            {
                // Sadece StartDate varsa, o tarihten itibaren olan uçuşları getir
                query = query.Where(f => f.Date.Date >= searchDto.StartDate.Value.Date);
            }
            else if (searchDto.EndDate.HasValue)
            {
                // Sadece EndDate varsa, o tarihe kadar olan uçuşları getir
                query = query.Where(f => f.Date.Date <= searchDto.EndDate.Value.Date);
            }

            if (searchDto.MinPrice.HasValue)
            {
                query = query.Where(f => f.Price >= searchDto.MinPrice.Value);
            }

            if (searchDto.MaxPrice.HasValue)
            {
                query = query.Where(f => f.Price <= searchDto.MaxPrice.Value);
            }

            if (searchDto.Capacity.HasValue)
            {
                query = query.Where(f => f.Capacity >= searchDto.Capacity.Value);
            }


            return await query.ToListAsync();
        }

    }
}
