using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Data;

namespace ProjectTracker.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _db;
        public ProjectRepository(AppDbContext db) => _db = db;

        public async Task<Project> AddAsync(Project project)
        {
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> GetByIdAsync(Guid id) =>
            await _db.Projects
                     .Include(p => p.Tasks)
                     .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Project>> ListByOwnerAsync(Guid ownerId) =>
            await _db.Projects
                     .Where(p => p.OwnerId == ownerId)
                     .ToListAsync();

        public async Task UpdateAsync(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
        }
    }
}
