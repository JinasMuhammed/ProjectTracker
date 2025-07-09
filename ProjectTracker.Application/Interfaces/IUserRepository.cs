using ProjectTracker.Domain.Entities;
using System.Threading.Tasks;

namespace ProjectTracker.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
