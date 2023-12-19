using Domain.Models;

namespace Infrastructure.Repositories.Birds
{
    public interface IBirdRepository
    {
        Task<Bird?> GetById(Guid id);
        Task<List<Bird>> GetAll();
        Task<Bird> Add(Bird bird);
        Task<Bird> Update(Bird bird);
        Task<Bird> Delete(Bird bird);
        void Save();
    }
}
