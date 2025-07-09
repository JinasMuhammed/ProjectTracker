using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Data;

namespace ProjectTracker.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;
        public TaskRepository(AppDbContext db) => _db = db;

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id) =>
            await _db.Tasks.FindAsync(id);

        public async Task<List<TaskItem>> ListByProjectAsync(Guid projectId) =>
            await _db.Tasks
                     .Where(t => t.ProjectId == projectId)
                     .ToListAsync();

        public async Task UpdateAsync(TaskItem task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }
    }
}
