
using Microsoft.AspNetCore.Mvc;
using OnlineVetAPI.DataModels;
using OnlineVetAPI.DomainModels;
using OnlineVetAPI.Helpers;
using OnlineVetAPI.Repositories;

namespace OnlineVetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;

        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }


        [HttpPost("register")]
        public async Task <IActionResult> Register([FromForm] Register request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            return Created("success", await userRepository.Create(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Login request)
        {
            var user = await userRepository.GetByEmail(request.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return BadRequest(new { message = "Invalid Credentials" });

            var jwt =  jwtService.GenerateToken(user.Id);

             Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "Success" });
        }

        [HttpGet("user")]

        public async Task<IActionResult> User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = jwtService.VerifyToken(jwt);

                int userId = int.Parse(token.Issuer);

                var user = await userRepository.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "success" });
        }
    }
}
