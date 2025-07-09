using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Application.Dtos
{
    public class CreateTaskDto
    {
        [Required] public string Title { get; set; } = null!;
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
