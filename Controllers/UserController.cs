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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, JwtService jwtService, ILogger<UserController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
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

            //_logger.LogInformation("User {Email} logged in successfully", loginDto.Email);

            Console.WriteLine("role",user.Role.ToString());    
            // Token üretme
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role.ToString());
            return Ok(new { Token = token });
        }


        // POST: api/user/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) // açıkça bodyden aldığımızı söylüyoruz birden fazla veri kaynağından parametre almamız gerekirse belirtmemiz daha iyidir. Ama Birden fazla veri kaynağı yoksa [FromBody] yazmasan da Asp.Net kendi anlıyor zaten. Dediğim gibi birden fazla parametre kullanacaksan nereden alacağını (body mi, query mi) belirtmen daha iyi olacaktır.
        {
            try
            {
                var existingUser = await _userService.GetUserByEmailAsync(registerDto.Email);
                if (existingUser != null)
                {
                    // Eğer email zaten kayıtlıysa uygun bir hata döndürüyoruz
                    return BadRequest(new { message = "Email already in use." });
                }

                var user = new User
                {
                    Name = registerDto.Username,
                    Email = registerDto.Email,
                    Password = registerDto.Password,  // Şifreleme eklenebilir
                    Role = "Client"
                };

                await _userService.AddUserAsync(user);
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
