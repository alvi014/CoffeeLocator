using CoffeeLocator.Application.DTOs.Auth;
using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;

namespace CoffeeLocator.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUnitOfWork unitOfWork, 
        IJwtService jwtService,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Method for user registration using Unit of Work.
    /// </summary>
    /// <param name="dto">The registration data.</param>
    /// <returns>Auth response with JWT token.</returns>
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        var passwordHash = _passwordHasher.Hash(dto.Password);

        var user = new User(dto.Email, passwordHash, dto.FullName);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto(user.Email, user.FullName, token, user.Role.ToString());
    }

    /// <summary>
    /// Method for user login using Unit of Work.
    /// </summary>
    /// <param name="dto">Login credentials.</param>
    /// <returns>Auth response if successful, otherwise null.</returns>
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

        if (user == null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            return null;

        var token = _jwtService.GenerateToken(user);
        return new AuthResponseDto(user.Email, user.FullName, token, user.Role.ToString());
    }
}