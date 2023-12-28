using Domain.Models.Animal;
using Infrastructure.MySQLDb;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Animals
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly RealDatabase _realDatabase;
        private readonly DbSet<AnimalModel> _dbSet;

        public AnimalRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
            _dbSet = _realDatabase.Set<AnimalModel>();
        }

        public async Task<AnimalModel> GetAnimalById(Guid id)
        {
            try
            {
                var wantedAnimal = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (wantedAnimal != null)
                {
                    return wantedAnimal;
                }
                else
                {
                    throw new KeyNotFoundException($"No animal found with Id {id} in the database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while getting an animal with Id {id} from the database", ex);
            }
        }
    }
}
