namespace CoffeeLocator.Application.DTOs.Auth;

public record LoginRequestDto(string Email, string Password);
public record RegisterRequestDto(string Email, string Password, string FullName);
public record AuthResponseDto(string Email, string FullName, string Token, string Role);