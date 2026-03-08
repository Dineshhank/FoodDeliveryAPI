using FoodDelivery.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FoodDelivery.Infrastructure.ExternalServices

{
    public class JwtTokenService: IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //    public string GenerateToken(Guid userId, List<string> roles)
        //    {
        //        var jwtSettings = _configuration.GetSection("JwtSettings");

        //        var keyValue = jwtSettings["Key"];
        //        var key = new SymmetricSecurityKey(
        //            Encoding.UTF8.GetBytes(keyValue));

        //        var credentials = new SigningCredentials(
        //            key, SecurityAlgorithms.HmacSha256);

        //        var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        //};

        //        // Add multiple roles
        //        foreach (var role in roles)
        //        {
        //            claims.Add(new Claim(ClaimTypes.Role, role));
        //        }

        //        var token = new JwtSecurityToken(
        //            issuer: jwtSettings["Issuer"],
        //            audience: jwtSettings["Audience"],
        //            claims: claims,
        //            expires: DateTime.UtcNow.AddMinutes(
        //                Convert.ToDouble(jwtSettings["DurationInMinutes"])),
        //            signingCredentials: credentials);

        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }

        public (string AccessToken, string Jti, DateTime Expiry)
    GenerateToken(Guid userId, List<string> roles)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var keyValue = jwtSettings["Key"];
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyValue));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // 🔥 Generate JTI (unique token id)
            var jti = Guid.NewGuid().ToString();

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, jti)
    };

            // Add multiple roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expiry = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(jwtSettings["DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: credentials);

            var accessToken =
                new JwtSecurityTokenHandler().WriteToken(token);

            return (accessToken, jti, expiry);
        }
    }
}
