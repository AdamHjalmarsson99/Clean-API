using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly RealDatabase _realDatabase;
        private readonly IConfiguration _configuration;
        //Added the const for JWT here. Am going ot look for a better and more flexibel option in the future.
        public const string Issuer = "https://localhost:7024";
        public const string Audience = "https://localhost:7024";
        public UserRepository(RealDatabase realDatabase, IConfiguration configuration)
        {
            _realDatabase = realDatabase;
            _configuration = configuration;
        }

        //GLÖM INTE ATT ÄNDRA I ALLA QUERIES
        public async Task<List<User>> GetAll()
        {
            //Tror att jag kan göra på detta viset
            return await _realDatabase.Users.ToListAsync();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _realDatabase.Users.FindAsync(id);
        }

        public async Task<User> Add(User user)
        {
            _realDatabase.Users.Add(user);
            await _realDatabase.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(User user)
        {
            _realDatabase.Users.Remove(user);
            await _realDatabase.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _realDatabase.Users.Update(user);
            await _realDatabase.SaveChangesAsync();
            return user;
        }

        public async Task<string> LogIn(string userName, string password)
        {
            var wantedUser = await _realDatabase.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);

            if (wantedUser == null)
            {
                return await Task.FromResult<string>(null!);
            }

            var token = GenerateJwtToken(wantedUser);

            return await Task.FromResult(token);
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

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
