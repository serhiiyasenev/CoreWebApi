using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LoginApi.Services
{
    public class JwtService
    {
        private readonly byte[] _secret;
        private readonly double _expirationSecond;
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig" + ":Secret").Value);
            _expirationSecond = Convert.ToDouble(_configuration.GetSection("JwtConfig" + ":ExpirationInSeconds").Value);
        }

        public TokenModel GetToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", username)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_expirationSecond),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), 
                                                        SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(tokenDescriptor));

            var token = new TokenModel
            {
                AccessToken = accessToken,
                ExpiresInSeconds = (int) _expirationSecond,
                TokenType = "Bearer"
            };

            return (token);

        }
    }
}
