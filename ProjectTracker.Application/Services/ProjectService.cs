using ProjectTracker.Application.Dtos;
using ProjectTracker.Application.Interfaces;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Repositories;

namespace ProjectTracker.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, Guid ownerId)
        {
            var entity = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = ownerId
            };

            var created = await _repo.AddAsync(entity);
            return new ProjectDto { Id = created.Id, Name = created.Name, Description = created.Description };
        }

        public async Task<List<ProjectDto>> ListForUserAsync(Guid ownerId)
        {
            var list = await _repo.ListByOwnerAsync(ownerId);
            return list.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null
                ? null
                : new ProjectDto { Id = p.Id, Name = p.Name, Description = p.Description };
        }

        public async Task UpdateAsync(Guid id, CreateProjectDto dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) throw new KeyNotFoundException("Project not found");
            p.Name = dto.Name;
            p.Description = dto.Description;
            await _repo.UpdateAsync(p);
        }

        public async Task DeleteAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) throw new KeyNotFoundException("Project not found");
            await _repo.DeleteAsync(p);
        }
    }
}
