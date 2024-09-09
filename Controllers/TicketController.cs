using FlightBookingSystem.DTO;
using FlightBookingSystem.Interfaces.IServices;
using FlightBookingSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseTicket([FromBody] TicketDto ticketDto)
        {
            var result = await _ticketService.PurchaseTicketAsync(ticketDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Failed to purchase ticket.");
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetUserTickets(int userId)
        {
            var tickets = await _ticketService.GetTicketsByUserIdAsync(userId);
            return Ok(tickets);
        }
    }

}
