
namespace CoffeeLocator.Application.DTOs.CoffeeShops;

public record CreateCoffeeShopDto(
    string Name,
    string GooglePlaceId, 
    string Address,
    double Latitude,
    double Longitude
);