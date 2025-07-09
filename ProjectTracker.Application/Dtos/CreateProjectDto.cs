using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Application.Dtos
{
    public class CreateProjectDto
    {
        [Required] public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
