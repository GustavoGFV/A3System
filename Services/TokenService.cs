using A3System.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace A3System.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string userId, string userRole)
        {
            var key = Encoding.ASCII.GetBytes("bd65c3-852*f850+0d*e29-588db5d0f85+4.6af++925f81df0As52");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId), // Identificação do usuário
                new Claim(ClaimTypes.Role, userRole) // Papel do usuário
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token válido por 1 hora
                Issuer = "EuMesmo", // Emissor do token
                Audience = "Clientes", // Audiência do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
