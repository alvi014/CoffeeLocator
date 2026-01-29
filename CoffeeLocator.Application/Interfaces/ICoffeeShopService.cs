using CoffeeLocator.Application.DTOs.CoffeeShops;

namespace CoffeeLocator.Application.Interfaces;

public interface ICoffeeShopService
{
    Task<CoffeeShopDetailDto> CreateCoffeeShopAsync(CreateCoffeeShopDto dto);


    Task<IEnumerable<CoffeeShopNearbyDto>> GetNearbyShopsAsync(double userLat, double userLng, double radiusInKm = 5);

    Task<CoffeeShopDetailDto?> GetShopByIdAsync(Guid id);

    Task<bool> DeleteAsync(Guid id);
}