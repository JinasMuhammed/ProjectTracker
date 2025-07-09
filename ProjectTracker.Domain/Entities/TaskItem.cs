﻿using System;

namespace ProjectTracker.Domain.Entities
{

    public class TaskItem
    {
 
        public Guid Id { get; set; }

  
        public string Title { get; set; } = null!;

 
        public string? Description { get; set; }

  
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }


        public Guid ProjectId { get; set; }

        public Project Project { get; set; } = null!;
    }
}
