using CoffeeLocator.Application.DTOs.CoffeeShops;

namespace CoffeeLocator.Application.Interfaces;

public interface ICoffeeShopService
{
    Task<CoffeeShopResponseDto> CreateCoffeeShopAsync(CreateCoffeeShopDto dto);
    Task<IEnumerable<CoffeeShopResponseDto>> GetNearbyShopsAsync(double userLat, double userLng);
    Task<CoffeeShopResponseDto?> GetShopByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}