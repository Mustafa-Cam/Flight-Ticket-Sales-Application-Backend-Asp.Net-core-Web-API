using FlightBookingSystem.DTO;
using FlightBookingSystem.DTO.FlightBookingSystem.DTO;
using FlightBookingSystem.Model;
using FlightBookingSystem.Services;
using FlightBookingSystem.Services.FlightBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // bu kullanım controller ismi ile değiştirilir mesela Sınıf adı UserController olsaydı api/user olacaktı. Burda ise api/flight.
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // GET: api/flight
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            var flights = await _flightService.GetFlightsAsync();
            return Ok(flights);
        }

        // GET: api/flight/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        // POST: api/flight
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddFlight(FlightDto flightDto)
        {

            // Tarih ve saat bilgilerini birleştir
            DateTime flightDateTime = DateTime.Parse($"{flightDto.Date} {flightDto.Time}");


            var flight = new Flight
            {
                Departure = flightDto.Departure,
                Arrival = flightDto.Arrival,
                Date = flightDateTime,
                Capacity = flightDto.Capacity,
                Price = flightDto.Price,
                IsActive = flightDto.IsActive
            };

            await _flightService.AddFlightAsync(flight);
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Id }, flight);
        }

        // PUT: api/flight/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlight(int id, FlightDto flightDto)
        {
            // Veritabanından güncellenecek uçuşu getir
            var existingFlight = await _flightService.GetFlightByIdAsync(id);
            if (existingFlight == null)
            {
                return NotFound(); // Uçuş bulunamadıysa 404 döndür
            }

            // FlightDto'daki tarih ve saat bilgilerini birleştirerek DateTime oluştur
            DateTime flightDateTime = DateTime.Parse($"{flightDto.Date} {flightDto.Time}");

            // Mevcut uçuş bilgilerini DTO'dan gelen bilgilerle güncelle
            existingFlight.Departure = flightDto.Departure;
            existingFlight.Arrival = flightDto.Arrival;
            existingFlight.Date = flightDateTime;
            existingFlight.Capacity = flightDto.Capacity;
            existingFlight.Price = flightDto.Price;
            existingFlight.IsActive = flightDto.IsActive;

            // Değişiklikleri kaydet
            await _flightService.UpdateFlightAsync(existingFlight);

            return NoContent(); // Güncelleme başarılıysa 204 döndür
        }


        // DELETE: api/flight/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            await _flightService.DeleteFlightAsync(id);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Flight>>> SearchFlights(
            [FromQuery] FlightSearchDto searchDto)
        {

            if (searchDto.StartDate.HasValue && searchDto.StartDate.Value.Date < DateTime.Today)
            {
                return BadRequest("Start date cannot be in the past.");
            }
            
            if (searchDto.EndDate.HasValue && searchDto.EndDate.Value.Date < DateTime.Today)
            {
                return BadRequest("End date cannot be in the past.");
            }

            var flights = await _flightService.SearchFlightsAsync(searchDto);
            return Ok(flights);
        }

    }
}
