using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CC.Passwordless.Utils.General
{
    public static class AuthenticationTokenGenerator
    {
        public static string GenerateJwtToken(string name, string email, IConfiguration _configuration)
        {
            var SecretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(SecretKey))
            {
                throw new InvalidOperationException("Jwt:SecretKey is not configured.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email),
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "http://localhost:4200", 
                Issuer = "https://localhost:7105",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
