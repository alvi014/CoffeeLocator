using CoffeeLocator.Application.DTOs.CoffeeShops;
using CoffeeLocator.Application.DTOs.Reviews;
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
    /// <param name="userLat">User's latitude</param>
    /// <param name="userLng">User's longitude</param>
    /// <param name="radiusInKm">Search radius</param>
    /// <returns>A list of nearby coffee shops with distance and rating</returns>
    public async Task<IEnumerable<CoffeeShopNearbyDto>> GetNearbyShopsAsync(double userLat, double userLng, double radiusInKm = 5)
    {
        var shops = await _repository.GetAllWithReviewsAsync();

        return shops.Select(s =>
        {
            var distance = CalculateHaversine(userLat, userLng, s.Latitude, s.Longitude);

            return new CoffeeShopNearbyDto(
                s.Id,
                s.Name,
                s.Address,
                s.Latitude,
                s.Longitude,
                Math.Round(distance, 2),
                s.AverageRating,
                s.TotalReviews,
                s.IsPremium,
                s.ImageUrl 
            );
        })
        .Where(s => s.DistanceInKm <= radiusInKm)
        .OrderBy(s => s.DistanceInKm)
        .ToList();
    }

    /// <summary>
    /// Metod <see langword="for"/> getting a CoffeeShop by its unique identifier.
    /// </summary>
    /// <param name="id">Id <see langword="for"/> the coffee shop</param>
    /// <returns>The detailed coffee shop data including reviews</returns>
    public async Task<CoffeeShopDetailDto?> GetShopByIdAsync(Guid id)
    {
        var shop = await _repository.GetByIdWithReviewsAsync(id);

        if (shop == null) return null;

        return new CoffeeShopDetailDto(
            shop.Id,
            shop.Name,
            shop.Description ?? "",
            shop.Address,
            shop.GooglePlaceId,
            shop.Latitude,
            shop.Longitude,
            shop.IsPremium,
            shop.AverageRating,
            shop.TotalReviews,
            shop.Reviews.Select(r => new ReviewResponseDto(
                r.Id,
                r.CreatedBy ?? "Usuario",
                r.Comment,
                r.Rating,
                r.CreatedAt
            )).ToList(),
            shop.ImageUrl 
        );
    }

    /// <summary>
    /// Metod <see langword="for"/> creating a new CoffeeShop entity and saving it to the repository.
    /// </summary>
    /// <param name="dto">DTO containing creation data</param>
    /// <returns>The created coffee shop details</returns>
    public async Task<CoffeeShopDetailDto> CreateCoffeeShopAsync(CreateCoffeeShopDto dto)
    {
        var shop = new CoffeeShop(dto.Name, dto.GooglePlaceId, dto.Address, dto.Latitude, dto.Longitude);


        shop.Description = dto.Description;
        shop.ImageUrl = dto.ImageUrl; 

        await _repository.AddAsync(shop);

        return new CoffeeShopDetailDto(
            shop.Id,
            shop.Name,
            shop.Description ?? "",
            shop.Address,
            shop.GooglePlaceId,
            shop.Latitude,
            shop.Longitude,
            shop.IsPremium,
            0, 
            0, 
            new List<ReviewResponseDto>(),
            shop.ImageUrl 
        );
    }

    /// <summary>
    /// Metod <see langword="for"/> deleting a CoffeeShop by its unique identifier.
    /// </summary>
    /// <param name="id">Guid of the shop to delete</param>
    /// <returns>True if deleted, false otherwise</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var shop = await _repository.GetByIdAsync(id);
        if (shop == null) return false;

        await _repository.DeleteAsync(shop);
        return true;
    }

    /// <summary>
    /// Metod <see langword="for"/> calculating the distance between two geographic coordinates using the Haversine formula.
    /// </summary>
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
}