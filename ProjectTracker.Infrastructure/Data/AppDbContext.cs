using Microsoft.EntityFrameworkCore;
using ProjectTracker.Domain.Entities;

namespace ProjectTracker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<User> Users { get; set; } = null!;

        
        public DbSet<Project> Projects { get; set; } = null!;

 
        public DbSet<TaskItem> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → Projects (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Project → Tasks (1:N)
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optionally configure table names, indexes, etc.
        }
    }
}
