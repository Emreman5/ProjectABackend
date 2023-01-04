using Core.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete;
using DataAccess.Concrete.Context;
using Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Security
{
    public class AccessTokenGenerator
    {
        public MsSqlDbContext _context { get; set; }
        public IConfiguration _config { get; set; }
        public CustomUser _user { get; set; }

        public AccessTokenGenerator(MsSqlDbContext context, IConfiguration config, CustomUser user)
        {
            _context = context;
            _config = config;
            _user = user;
        }

        private Token GeneterateToken()
        {
            DateTime expireDate = DateTime.Now.AddMinutes(1);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Application:Secret"]);
            var authRoles = from role in _context.Roles
                join userRole in _context.UserRoles
                    on role.Id equals userRole.RoleId
                where userRole.UserId == _user.Id
                select new { RoleName = role.Name };
            var authClaims = new List<Claim>();
            foreach (var item in authRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item.RoleName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config["Application:Audience"],
                Issuer = _config["Application:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, _user.Id),
                    new Claim(ClaimTypes.Name, _user.UserName),
                    new Claim(ClaimTypes.Email, _user.Email),
                }),

                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Subject.AddClaims(authClaims);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Token tokenInfo = new Token();
            tokenInfo.TokenBody = tokenString;
            tokenInfo.ExpireDate = expireDate;
            tokenInfo.RefreshToken = Guid.NewGuid().ToString();

            _user.RefreshToken = tokenInfo.RefreshToken;
            _user.RefreshTokenExpireDate = tokenInfo.ExpireDate.AddSeconds(30);
            _context.Users.Update(_user);
            _context.SaveChanges();
            return tokenInfo;
        }

        public ApplicationUserToken GetToken()
        {
            ApplicationUserToken userTokens = null;
            Token token = null;

            if (_context.AspNetUserTokens.Count(x => x.UserId == _user.Id) > 0)
            {
                userTokens = _context.AspNetUserTokens.FirstOrDefault(x => x.UserId == _user.Id);

                if (userTokens.ExpireDate <= DateTime.Now)
                {
                    token = GeneterateToken();

                    userTokens.ExpireDate = token.ExpireDate;
                    userTokens.Value = token.TokenBody;

                    _context.AspNetUserTokens.Update(userTokens);
                }
            }
            else
            {
                token = GeneterateToken();

                userTokens = new ApplicationUserToken();

                userTokens.UserId = _user.Id;
                userTokens.LoginProvider = "SystemAPI";
                userTokens.Name = _user.UserName;
                userTokens.ExpireDate = token.ExpireDate;
                userTokens.Value = token.TokenBody;

                _context.AspNetUserTokens.Add(userTokens);
            }

            _context.SaveChangesAsync();

            return userTokens;
        }

        public async Task<bool> DeleteToken()
        {
            bool result = true;

            try
            {
                if (_context.AspNetUserTokens.Count(x => x.UserId == _user.Id) > 0)
                {
                    ApplicationUserToken userTokens =
                        userTokens = _context.AspNetUserTokens.FirstOrDefault(x => x.UserId == _user.Id);

                    _context.AspNetUserTokens.Remove(userTokens);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<ApplicationUserToken> GetTokenByTokenValue(string token)
        {
            var appToken = await _context.AspNetUserTokens.FirstAsync(o => o.Value == token);
            return appToken;
        }

        public async Task<DateTime> GetTokenExpireDate(string refreshToken, CustomUser user)
        {
            var refToken = await _context.AspNetUserTokens.FirstOrDefaultAsync(t => t.UserId == user.Id);
            var result = refToken.ExpireDate;
            return result;
        }
    
}
}
