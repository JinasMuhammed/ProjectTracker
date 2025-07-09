using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem> AddAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<List<TaskItem>> ListByProjectAsync(Guid projectId);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
    }
}
