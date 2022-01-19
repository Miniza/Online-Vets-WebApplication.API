
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
        public IActionResult Register([FromForm] Register request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            return Created("success", userRepository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] Login request)
        {
            var user = userRepository.GetByEmail(request.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return BadRequest(new { message = "Invalid Credentials" });

            var jwt = jwtService.GenerateToken(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "Success" });
        }

        [HttpGet("user")]

        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = jwtService.VerifyToken(jwt);

                int userId = int.Parse(token.Issuer);

                var user = userRepository.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "success" });
        }
    }
}
