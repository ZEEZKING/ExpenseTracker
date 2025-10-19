using Application.DTOs.Auth.RequestModel;
using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class JWTManager : IJWTManager
    {
        public string CreateToken(string key, string issuer, string audience, JwtTokenRequest model)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, model.Id),
                new(ClaimTypes.Name, model.FullName),
                new(ClaimTypes.Email, model.Email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
