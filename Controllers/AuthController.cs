
using Microsoft.AspNetCore.Mvc;
using PatikaJWT.Dtos;
using PatikaJWT.Services;
using PatikaJWT.Models;
using PatikaJWT.Jwt;
using Microsoft.AspNetCore.Authorization;


namespace PatikaJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("add-user")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,



            };


            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var newLogin = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.LoginUser(newLogin);

            if (!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)

            });


            return Ok(new LoginResponse
            {
                Message = "Giriş başarıyla tamamlandı.",
                Token = token

            });

        }

        [HttpGet("Test")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Test()
        {
            return Ok("Giriş yetkisi verildi.");
        }
    }
}
