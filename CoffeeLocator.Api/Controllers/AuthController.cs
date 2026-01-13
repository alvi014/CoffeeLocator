using CoffeeLocator.Infrastructure.Persistence;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeLocator.Api.Controllers;

/// <summary>
/// Handles user authentication, including registration and login using JWT tokens.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="dto">The registration data including email, password, and full name.</param>
    /// <returns>A success message if the user is registered.</returns>
    /// <response code="200">User registered successfully.</response>
    /// <response code="400">If the email is already in use or data is invalid.</response>
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("The email is already registered.");

        // Hash password using BCrypt for secure storage
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        // Create user using the Domain Entity constructor (Defaults to StandardUser)
        var user = new User(dto.Email, passwordHash, dto.FullName);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="dto">Login credentials (Email and Password).</param>
    /// <returns>A JWT bearer token if credentials are valid.</returns>
    /// <response code="200">Returns the generated JWT token.</response>
    /// <response code="401">If credentials (email or password) are incorrect.</response>
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

        return Ok(GenerateToken(user));
    }

    /// <summary>
    /// Generates a JSON Web Token (JWT) for the authenticated user.
    /// </summary>
    /// <param name="user">The user entity to be encoded in the token claims.</param>
    /// <returns>A signed JWT string.</returns>
    private string GenerateToken(User user)
    {
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new Exception("JWT Key is missing in appsettings.json");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
           new Claim(ClaimTypes.Email, user.Email),
           new Claim(ClaimTypes.Name, user.FullName),
           new Claim(ClaimTypes.Role, user.Role.ToString()),
           new Claim("UserId", user.Id.ToString())
       };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials); 

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}