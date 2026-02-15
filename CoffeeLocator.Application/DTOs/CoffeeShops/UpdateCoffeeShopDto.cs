namespace CoffeeLocator.Application.DTOs.CoffeeShops;

public record UpdateCoffeeShopDto(
    Guid Id, 
    string Name,
    string? Description,
    string Address,
    double Latitude,
    double Longitude,
    string? ImageUrl,
    bool IsPremium 
);