using Domain.Models.Animal;

namespace Infrastructure.Repositories.Animals
{
    public interface IAnimalRepository
    {
        Task<AnimalModel> GetAnimalById(Guid id);
    }
}
