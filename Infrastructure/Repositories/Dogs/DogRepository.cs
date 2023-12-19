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
            //var listOfBirds = _realDatabase.Birds.ToList();
            //return await Task.FromResult(listOfBirds);

            //Tror att jag kan göra på detta viset
            return await _realDatabase.Dogs.ToListAsync();
        }

        public async Task<Dog?> GetById(Guid id)
        {
            //var bird = await _realDatabase.Birds.FindAsync(id);
            //return bird;
            return await _realDatabase.Dogs.FindAsync(id);
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
