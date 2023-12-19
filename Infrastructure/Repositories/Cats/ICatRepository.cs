using Domain.Models;

namespace Infrastructure.Repositories.Cats
{
    public interface ICatRepository
    {
        Task<Cat?> GetById(Guid id);
        Task<List<Cat>> GetAll();
        Task<Cat> Add(Cat cat);
        Task<Cat> Update(Cat cat);
        Task<Cat> Delete(Cat cat);
        void Save();
    }
}
