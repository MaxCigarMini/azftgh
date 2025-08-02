using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Model;
using System.ComponentModel.DataAnnotations;
using static ZAMETKI_FINAL.Abstraction.JWTOptions;

namespace ZAMETKI_FINAL.Services
{
    public class JwtTokenService
    {
        public class JwtToken
        {
            public int Id { get; set; }
            public required string Token { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime ExpiresAt { get; set; }
            // Поля для связки.
            public Guid UserId { get; set; }
            // default значение, которое выставит нам null, но не позволит обратится к полю.
            // Удобно,чтобы потом это заполнил контекст.
            public virtual User User { get; set; } = null!;
        }
        public class JwtTokenGenerator : IJwtTokenGenerator
        {
            private readonly JwtOptions _options = options.Value;
            public string Generate(User user)
            {
                // Настройки подписи токена.
                SigningCredentials credentials = new(
                // Расшифровываем секрет из настроек.
                new SymmetricSecurityKey(Convert.FromBase64String(_options.Secret)),
                SecurityAlgorithms.HmacSha256
                );
                // Клеймы (метаданные пользователя, которому выдаем токен).
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var now = DateTime.UtcNow;
                // Время жизни. По очевидным причинам не стоит иметь слишком большим.
                var expiration = now.AddMinutes(5);
                JwtSecurityToken securityToken = new(
                // Сервис, который подписал токен.
                issuer: _options.Issuer,
                 // Сервис, для которого подписан токен.
                audience: _options.Audience,
                expires: expiration,
                claims: claims,
                signingCredentials: credentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return new JwtToken
                {
                UserId = user.UserId,
                Token = token,
                CreatedAt = now,
                ExpiresAt = expiration,
                };
            }
        }

    }
}
