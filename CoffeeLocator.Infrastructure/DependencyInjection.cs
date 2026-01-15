using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.ExternalServices;
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
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no encontrada.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ICoffeeShopRepository, CoffeeShopRepository>();
        services.AddHttpClient<IGooglePlacesService, GooglePlacesService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}