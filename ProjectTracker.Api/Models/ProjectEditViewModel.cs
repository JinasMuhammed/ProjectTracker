using System;

namespace ProjectTracker.Api.Models
{
  
    public class ProjectEditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
