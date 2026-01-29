namespace CoffeeLocator.Application.DTOs.CoffeeShops;

public record CoffeeShopNearbyDto(
    Guid Id,
    string Name,
    string Address,
    double Latitude,
    double Longitude,
    double DistanceInKm,
    double AverageRating,
    int TotalReviews,
    bool IsPremium,
    string? ImageUrl
);