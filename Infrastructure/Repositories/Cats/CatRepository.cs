using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Cats
{
    public class CatRepository : ICatRepository
    {
        public readonly RealDatabase _realDatabase;
        public CatRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        //GLÖM INTE ATT ÄNDRA I ALLA QUERIES
        public async Task<List<Cat>> GetAll()
        {
            //Tror att jag kan göra på detta viset
            return await _realDatabase.Cats.ToListAsync();
        }

        public async Task<Cat?> GetById(Guid id)
        {
            return await _realDatabase.Cats.FindAsync(id);
        }

        public async Task<Cat> Add(Cat cat)
        {
            _realDatabase.Cats.Add(cat);
            await _realDatabase.SaveChangesAsync();
            return cat;
        }

        public async Task<Cat> Delete(Cat cat)
        {
            _realDatabase.Cats.Remove(cat);
            await _realDatabase.SaveChangesAsync();
            return cat;
        }

        public async Task<Cat> Update(Cat cat)
        {
            _realDatabase.Cats.Update(cat);
            await _realDatabase.SaveChangesAsync();
            return cat;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }
}
