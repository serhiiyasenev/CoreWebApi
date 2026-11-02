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
    public class JwtService(IConfiguration configuration)
    {
        private readonly byte[] _secret = GetSigningKey(configuration);
        private readonly string _issuer = configuration["JwtConfig:Issuer"]
            ?? throw new InvalidOperationException("JWT issuer is not configured.");
        private readonly string _audience = configuration["JwtConfig:Audience"]
            ?? throw new InvalidOperationException("JWT audience is not configured.");

        private static byte[] GetSigningKey(IConfiguration configuration)
        {
            var value = configuration.GetSection(SecretSectionName).Value;
            if (string.IsNullOrWhiteSpace(value) || Encoding.UTF8.GetByteCount(value) < 32)
                throw new InvalidOperationException("JWT signing key must be configured with at least 32 bytes.");
            return Encoding.UTF8.GetBytes(value);
        }
        private readonly double _expirationSecond = Convert.ToDouble(configuration.GetSection(ExpirationSectionName).Value);

        public TokenModel GetToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([new Claim("UserName", username)]),
                Expires = DateTime.UtcNow.AddSeconds(_expirationSecond),
                Issuer = _issuer,
                Audience = _audience,
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
