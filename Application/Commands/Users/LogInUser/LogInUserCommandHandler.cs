using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Commands.Users.LogInUser
{
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, string>
    {
        private readonly MockDatabase _mockDatabase;
        private readonly IConfiguration _configuration;
        public const string Issuer = "https://localhost:7024";
        public const string Audience = "https://localhost:7024";

        public LogInUserCommandHandler(MockDatabase mockDatabase, IConfiguration configuration)
        {
            _mockDatabase = mockDatabase;
            _configuration = configuration;
        }

        public Task<string> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var wantedUser = _mockDatabase.Users.FirstOrDefault(x => x.UserName == request.UserLogIn.UserName);

            if (wantedUser == null || wantedUser.Password != request.UserLogIn.Password)
            {
                return Task.FromResult<string>(null!);
            }

            var token = GenerateJwtToken(wantedUser);
            return Task.FromResult(token);
        }

        private string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

            if (user.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else claims.Add(new Claim(ClaimTypes.Role, "User"));

            var keyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

            // Ensure the key has the required size (256 bits for HS256)
            if (keyBytes.Length < 256 / 8)
            {

                var paddedKey = new byte[256 / 8];
                Array.Copy(keyBytes, paddedKey, keyBytes.Length);
                keyBytes = paddedKey;
            }

            var key = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

        }
    }
}
