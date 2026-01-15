namespace CoffeeLocator.Application.DTOs.CoffeeShops;

public record CoffeeShopResponseDto(
    Guid Id,
    string Name,
    string GooglePlaceId,
    string Address,
    double Latitude,
    double Longitude,
    bool IsPremium,
    double DistanceInKm,
    double AverageRating, 
    int TotalReviews      
);