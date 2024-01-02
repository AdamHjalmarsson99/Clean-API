using Domain.Models;

namespace Infrastructure.Repositories.Dogs
{
    public interface IDogRepository
    {
        Task<Dog?> GetById(Guid id);
        Task<List<Dog>> GetAll(string? breed, int? weight);
        Task<Dog> Add(Dog dog);
        Task<Dog> Update(Dog dog);
        Task<Dog> Delete(Dog dog);
        void Save();
    }
}
