using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public string GetToken(string username)
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

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
