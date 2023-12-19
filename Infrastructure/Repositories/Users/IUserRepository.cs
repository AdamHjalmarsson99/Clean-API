using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid id);
        Task<List<User>> GetAll();
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> Delete(User user);
        Task<string> LogIn(string userName, string password);
        void Save();
    }
}
