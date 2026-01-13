using CoffeeLocator.Infrastructure.Persistence;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Application.DTOs.CoffeeShops;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Api.Controllers;

/// <summary>
/// Provides endpoints for managing coffee shops in the system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CoffeeShopsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoffeeShopsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a complete list of all registered coffee shops.
    /// </summary>
    /// <returns>A list of coffee shop objects.</returns>
    /// <response code="200">Returns the list of coffee shops.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeShop>>> GetCoffeeShops()
    {
        return await _context.CoffeeShops.ToListAsync();
    }

    /// <summary>
    /// Retrieves the details of a specific coffee shop by its unique identifier.
    /// </summary>
    /// <param name="id">The GUID of the coffee shop.</param>
    /// <returns>The requested coffee shop data.</returns>
    /// <response code="200">If the coffee shop is found.</response>
    /// <response code="404">If no coffee shop matches the provided ID.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<CoffeeShop>> GetCoffeeShop(Guid id)
    {
        var coffeeShop = await _context.CoffeeShops.FindAsync(id);
        if (coffeeShop == null) return NotFound();
        return coffeeShop;
    }

    /// <summary>
    /// Registers a new coffee shop in the database.
    /// </summary>
    /// <param name="dto">The data required to create a coffee shop.</param>
    /// <returns>The newly created coffee shop.</returns>
    /// <response code="201">Returns the newly created item.</response>
    [HttpPost]
    public async Task<ActionResult<CoffeeShop>> PostCoffeeShop(CreateCoffeeShopDto dto)
    {
        var coffeeShop = new CoffeeShop(
            dto.Name,
            dto.GooglePlaceId,
            dto.Address,
            dto.Latitude,
            dto.Longitude
        );

        _context.CoffeeShops.Add(coffeeShop);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCoffeeShop), new { id = coffeeShop.Id }, coffeeShop);
    }

    /// <summary>
    /// Retrieves a list of coffee shops ordered by proximity to the user's current location.
    /// </summary>
    /// <param name="userLat">User's current latitude.</param>
    /// <param name="userLon">User's current longitude.</param>
    /// <returns>A list of coffee shops ordered by distance.</returns>
    [HttpGet("nearby")]
    public async Task<ActionResult<IEnumerable<CoffeeShopResponseDto>>> GetNearby(double userLat, double userLon)
    {
        var shops = await _context.CoffeeShops.ToListAsync();

        var nearbyShops = shops.Select(shop => new CoffeeShopResponseDto(
            shop.Id,
            shop.Name,
            shop.GooglePlaceId, 
            shop.Address,
            shop.Latitude,
            shop.Longitude,
            shop.IsPremium,    
            CalculateDistance(userLat, userLon, shop.Latitude, shop.Longitude)
        ))
        .OrderBy(s => s.DistanceInKm)
        .ToList();

        return Ok(nearbyShops);
    }

    /// <summary>
    /// Calculates the distance between two geographic coordinates using the Haversine formula.
    /// </summary>
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var r = 6371; 
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return r * c;
    }

    private double ToRadians(double angle) => Math.PI * angle / 180.0;
}