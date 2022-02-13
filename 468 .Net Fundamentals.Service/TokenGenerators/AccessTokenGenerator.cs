using _468_.Net_Fundamentals.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _468_.Net_Fundamentals.Service.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public AccessTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IEnumerable<Claim> authClaims)
        {
            /*var authClaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim("Name", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };*/

            var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var accessToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(5000),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

    }
}
