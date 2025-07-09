using ProjectTracker.Application.Dtos;
using ProjectTracker.Application.Interfaces;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<TaskDto> CreateAsync(Guid projectId, CreateTaskDto dto)
        {
            var entity = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                ProjectId = projectId,
                IsCompleted = false
            };

            var created = await _repo.AddAsync(entity);

            return new TaskDto
            {
                Id = created.Id,
                ProjectId = created.ProjectId,
                Title = created.Title,
                Description = created.Description,
                DueDate = created.DueDate,
                IsCompleted = created.IsCompleted
            };
        }

        public async Task<List<TaskDto>> ListByProjectAsync(Guid projectId)
        {
            var list = await _repo.ListByProjectAsync(projectId);
            return list.Select(t => new TaskDto
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted
            }).ToList();
        }

        public async Task<TaskDto?> GetByIdAsync(Guid id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) return null;

            return new TaskDto
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted
            };
        }

        public async Task UpdateAsync(Guid id, CreateTaskDto dto)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) throw new KeyNotFoundException("Task not found");

            t.Title = dto.Title;
            t.Description = dto.Description;
            t.DueDate = dto.DueDate;

            await _repo.UpdateAsync(t);
        }

        public async Task DeleteAsync(Guid id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) throw new KeyNotFoundException("Task not found");

            await _repo.DeleteAsync(t);
        }
    }
}
