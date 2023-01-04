using Core.Entities.Concrete;
using Core.Utilities.ResponseTypes;
using Microsoft.Extensions.Configuration;
using Model.Abstract;
using Model.DTO;


namespace Business.Abstract
{
    public interface IAuthService
    {
        public Task<IDataResult<AuthResponseDto>> Register(RegisterDto registerDto, IConfiguration config);
        public Task<IDataResult<AuthResponseDto>> Login(LoginDto loginDto, IConfiguration config);
        public Task<IDataResult<Token>> RefreshToken(string token, IConfiguration config);
        public Task<IDataResult<AuthResponseDto>> AuthMe(string token, string refreshToken, IConfiguration config);



    }
}
