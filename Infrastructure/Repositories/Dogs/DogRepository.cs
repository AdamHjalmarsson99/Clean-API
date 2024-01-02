using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace Infrastructure.Repositories.Dogs
{
    public class DogRepository : IDogRepository
    {
        public readonly RealDatabase _realDatabase;

        public DogRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task<List<Dog>> GetAll(string? breed, int? weight)
        {
            var query = _realDatabase.Dogs.AsQueryable();

            if (!string.IsNullOrEmpty(breed))
            {
                query = query.Where(dog => dog.Breed == breed);
            }

            if (weight.HasValue)
            {
                query = query.Where(dog => dog.Weight == weight.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Dog?> GetById(Guid id)
        {
            var dogWithUsers = await _realDatabase.Dogs
                .Where(dog => dog.Id == id)
                .Include(dog => dog.Users)
                .FirstOrDefaultAsync();

            return dogWithUsers;
        }

        public async Task<Dog> Add(Dog dog)
        {
            _realDatabase.Dogs.Add(dog);
            await _realDatabase.SaveChangesAsync();
            return dog;
        }

        public async Task<Dog> Delete(Dog dog)
        {
            _realDatabase.Dogs.Remove(dog);
            await _realDatabase.SaveChangesAsync();
            return dog;
        }

        public async Task<Dog> Update(Dog dog)
        {
            _realDatabase.Dogs.Update(dog);
            await _realDatabase.SaveChangesAsync();
            return dog;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
