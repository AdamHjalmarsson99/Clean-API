using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Dogs
{
    public class DogRepository : IDogRepository
    {
        public readonly RealDatabase _realDatabase;

        public DogRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        //GLÖM INTE ATT ÄNDRA I ALLA QUERIES
        public async Task<List<Dog>> GetAll()
        {
            var dogsWithUsers = await _realDatabase.Dogs
                .Include(dog => dog.Users)
                .ToListAsync();

            return dogsWithUsers;
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
