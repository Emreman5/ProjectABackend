using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _config = configuration;
        }

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = _authService.Register(dto, _config).Result;
            return Task.FromResult<IActionResult>(Ok(result));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto, _config);
            return Ok(result);
        }

        [HttpGet("Refresh")]
        public async Task<IActionResult> Refresh([FromQuery] string token)
        {
            var result = await _authService.RefreshToken(token, _config);
            if (result.IsSuccess == false)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpGet("AuthMe")]
        public async Task<IActionResult> Refresh([FromHeader] string token, string refreshToken)
        {
            var result = await _authService.AuthMe(token, refreshToken, _config);
            if (result.IsSuccess == false)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}
