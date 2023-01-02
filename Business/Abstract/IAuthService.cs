﻿using Core.Entities.Concrete;
using Core.Utilities.ResponseTypes;
using Microsoft.Extensions.Configuration;
using Model.Abstract;
using Model.DTO;


namespace Business.Abstract
{
    public interface IAuthService
    {
        public Task<IResult> Register(RegisterDto registerDto);
        public Task<IDataResult<AuthResponseDto>> Login(LoginDto loginDto, IConfiguration config);
        public IDataResult<Token> RefreshToken(string token, IConfiguration config);


    }
}
