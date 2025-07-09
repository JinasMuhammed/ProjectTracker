using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Infrastructure.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<Project?> GetByIdAsync(Guid id);
        Task<List<Project>> ListByOwnerAsync(Guid ownerId);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
    }
}
