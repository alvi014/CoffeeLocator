using CoffeeLocator.Application.DTOs.CoffeeShops;
using CoffeeLocator.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeLocator.Api.Controllers;

/// <summary>
/// Provides endpoints for managing coffee shops in the system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CoffeeShopsController : ControllerBase
{
    private readonly ICoffeeShopService _coffeeShopService;

    public CoffeeShopsController(ICoffeeShopService coffeeShopService)
    {
        _coffeeShopService = coffeeShopService;
    }

    /// <summary>
    /// Retrieves coffee shops using the nearby logic (defaulted to center coordinates).
    /// </summary>
    /// <returns>A list of coffee shop nearby objects.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoffeeShopNearbyDto>>> GetCoffeeShops()
    {
        // Usamos 0,0 o coordenadas por defecto para el listado general
        var shops = await _coffeeShopService.GetNearbyShopsAsync(0, 0, 20000); // Radio grande para traer todas
        return Ok(shops);
    }

    /// <summary>
    /// Retrieves the details of a specific coffee shop by its identifier.
    /// </summary>
    /// <param name="id">The GUID of the coffee shop.</param>
    /// <returns>The requested coffee shop detailed data.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CoffeeShopDetailDto>> GetCoffeeShop(Guid id)
    {
        var coffeeShop = await _coffeeShopService.GetShopByIdAsync(id);

        if (coffeeShop == null) return NotFound();

        return Ok(coffeeShop);
    }

    /// <summary>
    /// Retrieves a list of coffee shops ordered by proximity to the user's current location.
    /// </summary>
    /// <param name="userLat">User's current latitude.</param>
    /// <param name="userLon">User's current longitude.</param>
    /// <param name="radius">Radius in Km (optional).</param>
    /// <returns>A list of coffee shops with distance and rating information.</returns>
    [HttpGet("nearby")]
    public async Task<ActionResult<IEnumerable<CoffeeShopNearbyDto>>> GetNearby(double userLat, double userLon, double radius = 5)
    {
        var nearbyShops = await _coffeeShopService.GetNearbyShopsAsync(userLat, userLon, radius);
        return Ok(nearbyShops);
    }

    /// <summary>
    /// Registers a new coffee shop in the system.
    /// </summary>
    /// <param name="dto">The data required to create a coffee shop.</param>
    /// <returns>The newly created coffee shop details.</returns>
    [HttpPost]
    public async Task<ActionResult<CoffeeShopDetailDto>> PostCoffeeShop(CreateCoffeeShopDto dto)
    {
        var result = await _coffeeShopService.CreateCoffeeShopAsync(dto);

        return CreatedAtAction(nameof(GetCoffeeShop), new { id = result.Id }, result);
    }

    /// <summary>
    /// Deletes a coffee shop by its unique identifier. Only accessible to users with the "Admin" role.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _coffeeShopService.DeleteAsync(id);

        if (!result) return NotFound("No se encontró la cafetería.");

        return NoContent();
    }
}