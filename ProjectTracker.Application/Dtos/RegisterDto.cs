using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Application.Dtos
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
    }
}
