using System;
using System.Collections.Generic;

namespace ProjectTracker.Domain.Entities
{

    public class Project
    {
     
        public Guid Id { get; set; }

      
        public string Name { get; set; } = null!;

     
        public string? Description { get; set; }

     
        public Guid OwnerId { get; set; }

      
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
