using Domain.Models;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Birds
{
    public class BirdRepository : IBirdRepository
    {
        public readonly RealDatabase _realDatabase;

        public BirdRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        //GLÖM INTE ATT ÄNDRA I ALLA QUERIES
        public async Task<List<Bird>> GetAll()
        {
            var birdsWithUsers = await _realDatabase.Birds
                  .Include(bird => bird.Users)
                  .ToListAsync();

            return birdsWithUsers;
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
