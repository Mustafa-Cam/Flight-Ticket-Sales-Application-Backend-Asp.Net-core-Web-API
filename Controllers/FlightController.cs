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
    [Route("api/[controller]")]
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
        public async Task<ActionResult> AddFlight(Flight flight)
        {
            await _flightService.AddFlightAsync(flight);
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Id }, flight);
        }

        // PUT: api/flight/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlight(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            await _flightService.UpdateFlightAsync(flight);
            return NoContent();
        }

        // DELETE: api/flight/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            await _flightService.DeleteFlightAsync(id);
            return NoContent();
        }

       
    }
}
