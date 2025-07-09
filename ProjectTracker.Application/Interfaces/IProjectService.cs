using ProjectTracker.Application.Dtos;

namespace ProjectTracker.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateAsync(CreateProjectDto dto, Guid ownerId);
        Task<List<ProjectDto>> ListForUserAsync(Guid ownerId);
        Task<ProjectDto?> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, CreateProjectDto dto);
        Task DeleteAsync(Guid id);
    }
}
