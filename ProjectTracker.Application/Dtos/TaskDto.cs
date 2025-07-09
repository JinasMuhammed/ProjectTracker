﻿namespace ProjectTracker.Application.Dtos
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
