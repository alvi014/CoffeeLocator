using AutoMapper;
using CoffeeLocator.Application.DTOs.CoffeeShops;
using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;

namespace CoffeeLocator.Application.Services;

public class CoffeeShopService : ICoffeeShopService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CoffeeShopService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Metod to retrieve nearby coffee shops based on user's location and specified radius. It calculates the distance using the Haversine formula and returns a list of shops within the radius, sorted by proximity.
    /// </summary>
    /// <param name="userLat"></param>
    /// <param name="userLng"></param>
    /// <param name="radiusInKm"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CoffeeShopNearbyDto>> GetNearbyShopsAsync(double userLat, double userLng, double radiusInKm = 5)
    {
        var shops = await _unitOfWork.CoffeeShops.GetAllWithReviewsAsync();

        var shopDtos = shops.Select(s =>
        {
            var distance = CalculateHaversine(userLat, userLng, s.Latitude, s.Longitude);
            var dto = _mapper.Map<CoffeeShopNearbyDto>(s);

            return dto with { DistanceInKm = Math.Round(distance, 2) };
        })
        .Where(s => s.DistanceInKm <= radiusInKm)
        .OrderBy(s => s.DistanceInKm)
        .ToList();

        return shopDtos;
    }

    /// <summary>
    /// Metod to retrieve detailed information about a specific coffee shop by its ID. It includes all relevant details such as name, description, address, average rating, and recent reviews. If the shop is not found, it returns null.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<CoffeeShopDetailDto?> GetShopByIdAsync(Guid id)
    {
        var shop = await _unitOfWork.CoffeeShops.GetByIdWithReviewsAsync(id);
        return _mapper.Map<CoffeeShopDetailDto>(shop);
    }

    /// <summary>
    /// Metod to create a new coffee shop in the system. It takes a DTO containing the necessary information, maps it to the CoffeeShop entity, and saves it to the database. After successful creation, it returns the details of the newly created shop.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<CoffeeShopDetailDto> CreateCoffeeShopAsync(CreateCoffeeShopDto dto)
    {
        var shop = _mapper.Map<CoffeeShop>(dto);

        await _unitOfWork.CoffeeShops.AddAsync(shop);
        await _unitOfWork.SaveChangesAsync(); 

        return _mapper.Map<CoffeeShopDetailDto>(shop);
    }

    /// <summary>
    /// Metod to update an existing coffee shop's information. It retrieves the shop by ID, updates its properties based on the provided DTO, and saves the changes to the database. If the shop is not found, it returns false; otherwise, it returns true after a successful update.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<bool> UpdateShopAsync(UpdateCoffeeShopDto dto)
    {
        var shop = await _unitOfWork.CoffeeShops.GetByIdAsync(dto.Id);
        if (shop == null) return false;
        _mapper.Map(dto, shop);
        await _unitOfWork.CoffeeShops.UpdateAsync(shop);

        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Deletes a coffee shop from the system based on its ID. It first checks if the shop exists, and if it does, it removes it from the database and commits the changes. If the shop is not found, it returns false; otherwise, it returns true after successful deletion.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var shop = await _unitOfWork.CoffeeShops.GetByIdAsync(id);
        if (shop == null) return false;

        await _unitOfWork.CoffeeShops.DeleteAsync(shop);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Metod to calculate the distance between two geographic coordinates (latitude and longitude) using the Haversine formula. This formula accounts for the curvature of the Earth, providing an accurate distance measurement in kilometers. The method takes the latitude and longitude of two points and returns the distance between them.
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
}