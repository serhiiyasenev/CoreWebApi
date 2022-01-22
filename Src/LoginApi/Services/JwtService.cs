using LoginApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Common.Storages.StringStorage;

namespace LoginApi.Services
{
    public class JwtService
    {
        private readonly byte[] _secret;
        private readonly double _expirationSecond;

        public JwtService(IConfiguration configuration)
        {
            _secret = Encoding.ASCII.GetBytes(configuration.GetSection(SecretSectionName).Value);
            _expirationSecond = Convert.ToDouble(configuration.GetSection(ExpirationSectionName).Value);
        }

        public TokenModel GetToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("UserName", username)}),
                Expires = DateTime.UtcNow.AddSeconds(_expirationSecond),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(tokenDescriptor));

            var token = new TokenModel
            {
                AccessToken = accessToken,
                ExpiresInSeconds = (int) _expirationSecond,
                TokenType = "Bearer"
            };

            return token;
        }
    }
}
