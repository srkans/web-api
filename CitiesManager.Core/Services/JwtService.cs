using CitiesManager.Core.DTO;
using CitiesManager.Core.Identity;
using CitiesManager.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[] //payload
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), //user identity
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //unique id for the token
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), //Issued at(date and time of token generation)
                new Claim(ClaimTypes.NameIdentifier, user.Email), //unique name identifier of the user (email) -optional
                new Claim(ClaimTypes.Name, user.PersonName) //name of the user - optional
            };

            //Jwt only use symmetric algorithm
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                   );

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse() 
            { 
                Token = token, 
                Email = user.Email, 
                PersonName = user.PersonName, 
                Expiration = expiration, 
                RefreshToken = GenerateRefreshToken(), 
                RefreshTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"])) 
            };
            //email username ve expiration'ı client tarafında kolaylıkla okuyabilmek için ekledik.
        }

        private static string GenerateRefreshToken()
        {
            Byte[] bytes = new byte[64];

            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
