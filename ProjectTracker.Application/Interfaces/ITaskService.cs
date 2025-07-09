using ProjectTracker.Application.Dtos;

namespace ProjectTracker.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(Guid projectId, CreateTaskDto dto);
        Task<List<TaskDto>> ListByProjectAsync(Guid projectId);
        Task<TaskDto?> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, CreateTaskDto dto);
        Task DeleteAsync(Guid id);
    }
}
