using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string userName,string userId)
        {
            var keyString = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key 未設定");
            var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer 未設定");
            var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience 未設定");

            var claims = new[]
            {
                new Claim("userName", userName),
                new Claim("Id", userId)
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
