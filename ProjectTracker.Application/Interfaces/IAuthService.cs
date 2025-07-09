using ProjectTracker.Application.Dtos;

namespace ProjectTracker.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string?> ValidateUserAndGenerateTokenAsync(LoginDto dto);
    }
}