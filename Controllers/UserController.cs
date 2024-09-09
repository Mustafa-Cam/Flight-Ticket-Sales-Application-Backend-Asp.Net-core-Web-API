using FlightBookingSystem.DTO;
using FlightBookingSystem.Model;
using FlightBookingSystem.Services;
using FlightBookingSystem.Services.FlightBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public UserController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // POST: api/user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Token üretme
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role.ToString());
            return Ok(new { Token = token });
        }


        // POST: api/user/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) // açıkça bodyden aldığımızı söylüyoruz birden fazla veri kaynağından parametre almamız gerekirse belirtmemiz daha iyidir. Ama Birden fazla veri kaynağı yoksa [FromBody] yazmasan da Asp.Net kendi anlıyor zaten. Dediğim gibi birden fazla parametre kullanacaksan nereden alacağını (body mi, query mi) belirtmen daha iyi olacaktır.
        {
            var user = new User
            {
                Name = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password,  // Şifreleme eklenebilir
                Role = "Client"
            };

            await _userService.AddUserAsync(user);
            return Ok("User registered successfully.");
        }



        // GET: api/user/{id}
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/user/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult> AddUser(User user) // Bak mesela burda [FromBody] yazmamışız gördüğün gibi. 
        {
            await _userService.AddUserAsync(user); // save to db
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user); // result of api ex: 201 
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
