using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Infrastructure.Repositories.Birds
{
    public class BirdRepository : IBirdRepository
    {
        public readonly RealDatabase _realDatabase;

        public BirdRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task<List<Bird>> GetAll(string? Color)
        {
            try
            {
                var query = _realDatabase.Birds.Include(bird => bird.Users).AsQueryable();

                if (!string.IsNullOrEmpty(Color))
                {
                    query = query.Where(bird => bird.Color == Color);
                }

                var birdsWithUsers = await query.ToListAsync();

                return birdsWithUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BirdRepository.GetAll: {ex.Message}");
                throw;
            }
        }

        public async Task<Bird?> GetById(Guid id)
        {
            var birdWithUsers = await _realDatabase.Birds
                .Where(bird => bird.Id == id)
                .Include(bird => bird.Users)
                .FirstOrDefaultAsync();

            return birdWithUsers;
        }

        public async Task<Bird> Add(Bird bird)
        {
            _realDatabase.Birds.Add(bird);
            await _realDatabase.SaveChangesAsync();
            return bird;
        }

        public async Task<Bird> Delete(Bird bird)
        {
            _realDatabase.Birds.Remove(bird);
            await _realDatabase.SaveChangesAsync();
            return bird;
        }

        public async Task<Bird> Update(Bird bird)
        {
            _realDatabase.Birds.Update(bird);
            await _realDatabase.SaveChangesAsync();
            return bird;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
