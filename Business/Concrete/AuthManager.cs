using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants.Messages;
using Business.Security;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.ResponseTypes;
using DataAccess.Concrete;
using DataAccess.Concrete.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Abstract;
using Model.DTO;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IUnitOfWork _unitOfWork;

        public AuthManager(UserManager<CustomUser> userManager, 
            SignInManager<CustomUser> signInManager, 
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<AuthResponseDto>> Register(RegisterDto registerDto, IConfiguration config)
        {
           var logic = BusinessRules.Run(UserExistsEmail(registerDto.Email).Result);
            if (logic.IsSuccess == false)
                return await Task.FromResult(new ErrorDataResult<AuthResponseDto>(logic.Message));
            var user = new CustomUser()
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName
            };
            await _userManager.CreateAsync(user, registerDto.Password);
            await DefaultRole();
            await _userManager.AddToRoleAsync(user, "User");

            var result = await Login(new LoginDto() { Email = registerDto.Email, Password = registerDto.Password }, config);
            return result;

        }

        public async Task<IDataResult<AuthResponseDto>> Login(LoginDto loginDto, IConfiguration config)
        {
            var user =  await _userManager.FindByEmailAsync(loginDto.Email);
            var tokenGenerator = new AccessTokenGenerator(_unitOfWork.GetContext(), config, user);
            var logic = BusinessRules.Run(await SignIn(user, loginDto.Password));
            if (logic.IsSuccess == false)
                return new ErrorDataResult<AuthResponseDto>(logic.Message);
            ApplicationUserToken userTokens =  tokenGenerator.GetToken();
            var roles =  await _userManager.GetRolesAsync(user);
            var token = new Token()
            {
                TokenBody = userTokens.Value,
                ExpireDate = userTokens.ExpireDate,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpireDate = user.RefreshTokenExpireDate
            };
            var result = new AuthResponseDto()
            {
                Email = user.Email, Roles = roles.ToList(), Token = token, UserId = user.Id, Status = true
            };
            return new SuccessDataResult<AuthResponseDto>(result);
        }

        public async Task<IDataResult<Token>> RefreshToken(string token, IConfiguration config)
        {
            var context = _unitOfWork.GetContext();
            var user =  await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == token);
            var accessTokenGenerator = new AccessTokenGenerator(context, config, user);
            var expireDate = await accessTokenGenerator.GetTokenExpireDate(token, user);
            if (expireDate < DateTime.Now && user.RefreshTokenExpireDate!.Value > DateTime.Now)
            {
                
                var userToken = accessTokenGenerator.GetToken();
                var result = new Token()
                {
                    TokenBody = userToken.Value, ExpireDate = userToken.ExpireDate, RefreshToken = user.RefreshToken,
                    RefreshTokenExpireDate = user.RefreshTokenExpireDate
                };
                return new SuccessDataResult<Token>(result);
            } 
            if (user.RefreshTokenExpireDate.Value < DateTime.Now)
            {
                return new ErrorDataResult<Token>(AuthMessages.TryAgain);
            }

            return new ErrorDataResult<Token>(AuthMessages.TokenStillUsable);
        }

        public async Task<IDataResult<AuthResponseDto>> AuthMe(string token, string refreshToken, IConfiguration config)
        {
            var refreshResult  = await RefreshToken(refreshToken, config);
            CustomUser user;
            IList<string> roles;
            if (!refreshResult.IsSuccess && refreshResult.Message == AuthMessages.TryAgain)
            {
                return new ErrorDataResult<AuthResponseDto>(AuthMessages.TryAgain);
            }
            var response = new AuthResponseDto();
            if (!refreshResult.IsSuccess)
            {
                user = await _userManager.Users.FirstAsync(u => u.RefreshToken == refreshToken);
                roles = await _userManager.GetRolesAsync(user);
                
                var tokenObj = await _unitOfWork.GetTokenByTokenValue(token);
                var oldToken = new Token(){
                    ExpireDate = tokenObj.ExpireDate, 
                    RefreshToken = user.RefreshToken, 
                    RefreshTokenExpireDate = user.RefreshTokenExpireDate, 
                    TokenBody = token};

                response.SetUser(user, roles.ToList(), oldToken, "A" );
                return new SuccessDataResult<AuthResponseDto>(response);
            }
            user = await _userManager.Users.FirstAsync(u => u.RefreshToken == refreshResult.Data.RefreshToken);
            roles = await _userManager.GetRolesAsync(user);
            response.SetUser(user, roles.ToList(), refreshResult.Data, "X");
            return new SuccessDataResult<AuthResponseDto>(response);
        }


        private async Task<IResult> UserExistsEmail(string email)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return new ErrorResult(AuthMessages.EmailAlreadyInUse);
            }
            return new SuccessResult();
        }

        private async Task DefaultRole()
        {
            bool roleExists = await _roleManager.RoleExistsAsync("User");

            if (!roleExists)
            {
                IdentityRole role = new IdentityRole("User");
                role.NormalizedName = "User";

                _roleManager.CreateAsync(role).Wait();
            }
        }

        private async Task<IResult> SignIn(CustomUser user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (result.Succeeded == false)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
