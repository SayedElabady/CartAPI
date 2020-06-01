using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Contracts;
using WebApplication.Models;
using WebApplication.Options;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepo _userManager;
        private readonly JwtSettings _jwtSettings;

        public UsersService(IUsersRepo userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> Register(string email, string password, string name)
        {
            var existedUser = await _userManager.FindUserByEmail(email);

            if (existedUser != null)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] {"Email already exists "}
                };
            }

            var newUser = new User()
            {
                Name = name,
                Email = email
            };
            
          await _userManager.Create(newUser);
            

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}