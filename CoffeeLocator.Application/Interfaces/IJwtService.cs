using CoffeeLocator.Domain.Entities;

namespace CoffeeLocator.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}