using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoreWebApp.Authorization.Services
{
    public class JwtServicecs
    {
        private readonly byte[] _secret;
        private readonly double _expirationSecond;

        public JwtServicecs(IConfiguration configuration)
        {
            _secret = Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfig:Secret").Value);
            _expirationSecond = Convert.ToDouble(configuration.GetSection("JwtConfig:ExpirationSecond").Value);
        }

        public string GetToken(string username, string lastName, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", username),
                    new Claim("LastName", lastName),
                }),
                Expires = DateTime.UtcNow.AddSeconds(_expirationSecond),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
