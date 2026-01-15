using CoffeeLocator.Application.DTOs.Auth;
using CoffeeLocator.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeLocator.Api.Controllers;

/// <summary>
/// Handles user authentication using Clean Architecture services.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="dto">The registration data.</param>
    /// <response code="200">Returns the auth data and token.</response>
    /// <response code="400">If the email is already in use.</response>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequestDto dto)
    {
        try
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token with user roles.
    /// </summary>
    /// <param name="dto">Login credentials.</param>
    /// <response code="200">Returns the generated JWT token and user info.</response>
    /// <response code="401">If credentials are incorrect.</response>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized("Credenciales inválidas.");

        return Ok(result);
    }
}