using CoffeeLocator.Application.DTOs.Auth;

namespace CoffeeLocator.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto);
}