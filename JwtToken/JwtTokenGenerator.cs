using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaAPI.JwtToken
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration to access settings from appsettings.json
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Generates a JWT token for a given user ID and username
        public string GenerateToken(int userId, string username)
        {
            // Define the claims to include in the token
            var claims = new[]
            {
                // Using ClaimTypes.NameIdentifier for the user ID
                 new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                 new Claim(ClaimTypes.Name, username),
                 // Standard JWT claims
                 new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                 new Claim(JwtRegisteredClaimNames.UniqueName, username)
            };

            // Read the secret key from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? throw new Exception("JWT key not found in configuration")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token with claims and expiration
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            // Return the final token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
