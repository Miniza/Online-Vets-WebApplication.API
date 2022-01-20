
using Microsoft.AspNetCore.Mvc;
using OnlineVetAPI.DataModels;
using OnlineVetAPI.DomainModels;
using OnlineVetAPI.Helpers;
using OnlineVetAPI.Interfaces;

namespace OnlineVetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;

        public AuthController(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            this.unitOfWork = unitOfWork;
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
            await unitOfWork.SaveAsync();
            return Created("success", await unitOfWork.UserRepository.Create(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Login request)
        {
            var user = await unitOfWork.UserRepository.GetByEmail(request.Email);
            await unitOfWork.SaveAsync();

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

                var user = await unitOfWork.UserRepository.GetById(userId);
                await unitOfWork.SaveAsync();

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
