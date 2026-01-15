using System.Security.Claims;
using CoffeeLocator.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CoffeeLocator.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Metod for getting the current user's email.
    /// </summary>
    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}