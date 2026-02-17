using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using CoffeeLocator.Infrastructure.Repositories;
using CoffeeLocator.Infrastructure.Security;
using CoffeeLocator.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeLocator.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Injects infrastructure services into the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration manager.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // --- Database Configuration ---
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // --- Data Persistence (Pattern) ---
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // --- Security and Identity ---
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        // --- System Context ---
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}