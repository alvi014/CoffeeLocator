using CoffeeLocator.Application.DTOs.CoffeeShops;
using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;

namespace CoffeeLocator.Application.Services;

public class CoffeeShopService : ICoffeeShopService
{
    private readonly ICoffeeShopRepository _repository;
    private readonly IGooglePlacesService _googlePlacesService;

    public CoffeeShopService(ICoffeeShopRepository repository, IGooglePlacesService googlePlacesService)
    {
        _repository = repository;
        _googlePlacesService = googlePlacesService;
    }

    /// <summary>
    /// Metod <see langword="for"/> getting all CoffeeShops including their reviews and calculating the distance from the user's location.
    /// </summary>
    /// <param name="userLat"></param>
    /// <param name="userLng"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CoffeeShopResponseDto>> GetNearbyShopsAsync(double userLat, double userLng)
    {
        var shops = await _repository.GetAllWithReviewsAsync();

        return shops.Select(s =>
        {
            var distance = CalculateHaversine(userLat, userLng, s.Latitude, s.Longitude);

            return new CoffeeShopResponseDto(
                s.Id,
                s.Name,
                s.GooglePlaceId,
                s.Address,
                s.Latitude,
                s.Longitude,
                s.IsPremium,
                Math.Round(distance, 2),
                s.AverageRating,
                s.TotalReviews
            );
        })
        .OrderBy(s => s.DistanceInKm)
        .ToList();
    }

    /// <summary>
    /// Metod <see langword="for"/> getting a CoffeeShop by its unique identifier.
    /// </summary>
    /// <param name="id">Id <see langword="for"/></param>
    /// <returns></returns>
    public async Task<CoffeeShopResponseDto?> GetShopByIdAsync(Guid id)
    {
        var shop = await _repository.GetByIdWithReviewsAsync(id);

        if (shop == null) return null;

        return new CoffeeShopResponseDto(
            shop.Id,
            shop.Name,
            shop.GooglePlaceId,
            shop.Address,
            shop.Latitude,
            shop.Longitude,
            shop.IsPremium,
            0, 
            shop.AverageRating,
            shop.TotalReviews
        );
    }

    /// <summary>
    /// Metod <see langword="for"/> calculating the distance between two geographic coordinates using the Haversine formula.    
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lon1"></param>
    /// <param name="lat2"></param>
    /// <param name="lon2"></param>
    /// <returns></returns>
    private double CalculateHaversine(double lat1, double lon1, double lat2, double lon2)
    {
        var d1 = lat1 * (Math.PI / 180.0);
        var num1 = lon1 * (Math.PI / 180.0);
        var d2 = lat2 * (Math.PI / 180.0);
        var num2 = (lon2 * (Math.PI / 180.0)) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                 Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

        return 6371.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }

    /// <summary>
    /// Metod <see langword="for"/> creating a new CoffeeShop entity and saving it to the repository.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<CoffeeShopResponseDto> CreateCoffeeShopAsync(CreateCoffeeShopDto dto)
    {
        var shop = new CoffeeShop(dto.Name, dto.GooglePlaceId, dto.Address, dto.Latitude, dto.Longitude);
        await _repository.AddAsync(shop); 

        return new CoffeeShopResponseDto(shop.Id, shop.Name, shop.GooglePlaceId, shop.Address,
                                       shop.Latitude, shop.Longitude, shop.IsPremium, 0, 0, 0);
    }

    /// <summary>
    /// Metod <see langword="for"/> deleting a CoffeeShop by its unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var shop = await _repository.GetByIdAsync(id);
        if (shop == null) return false;

        await _repository.DeleteAsync(shop);
        return true;
    }
}