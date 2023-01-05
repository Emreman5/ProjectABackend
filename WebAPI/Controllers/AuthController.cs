using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Abstract;
using Model.DTO;

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
        public  async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result =  await _authService.Register(dto, _config);
            return Ok(result);
        }
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAdminUser(dto, _config);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto, _config);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
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
        public async Task<IActionResult> AuthMe([FromHeader] string token, [FromHeader] string refreshToken)
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
