using CoffeeLocator.Application.DTOs.Auth;
using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;

namespace CoffeeLocator.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordHasher passwordHasher) 
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Metod for user registration.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        var passwordHash = _passwordHasher.Hash(dto.Password);

        var user = new User(dto.Email, passwordHash, dto.FullName);

        await _userRepository.AddAsync(user);
        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto(user.Email, user.FullName, token, user.Role.ToString());
    }

    /// <summary>
    /// Metod for user login.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            return null;

        var token = _jwtService.GenerateToken(user);
        return new AuthResponseDto(user.Email, user.FullName, token, user.Role.ToString());
    }
}