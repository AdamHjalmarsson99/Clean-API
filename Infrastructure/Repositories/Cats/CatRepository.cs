using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace Infrastructure.Repositories.Cats
{
    public class CatRepository : ICatRepository
    {
        public readonly RealDatabase _realDatabase;
        public CatRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task<List<Cat>> GetAll(string? breed, int? weight)
        {
            var query = _realDatabase.Cats.AsQueryable();

            if (!string.IsNullOrEmpty(breed))
            {
                query = query.Where(cat => cat.Breed == breed);
            }

            if (weight.HasValue)
            {
                query = query.Where(cat => cat.Weight == weight.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Cat?> GetById(Guid id)
        {
            var catWithUsers = await _realDatabase.Cats
                .Where(cat => cat.Id == id)
                .Include(cat => cat.Users)
                .FirstOrDefaultAsync();

            return catWithUsers;
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
