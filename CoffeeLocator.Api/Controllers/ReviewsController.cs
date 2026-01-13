using CoffeeLocator.Application.DTOs.Reviews;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReviewsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new review to a coffee shop.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> PostReview(CreateReviewDto dto)
    {
        // Validar que la cafetería exista
        var shopExists = await _context.CoffeeShops.AnyAsync(s => s.Id == dto.CoffeeShopId);
        if (!shopExists) return NotFound("The specified coffee shop does not exist.");

        // Validar rango de calificación
        if (dto.Rating < 1 || dto.Rating > 5) return BadRequest("Rating must be between 1 and 5.");

        var review = new Review(dto.UserId, dto.Comment, dto.Rating, dto.CoffeeShopId);

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return Ok("Review added successfully.");
    }

    /// <summary>
    /// Gets all reviews for a specific coffee shop.
    /// </summary>
    [HttpGet("shop/{coffeeShopId}")]
    public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetReviewsByShop(Guid coffeeShopId)
    {
        return await _context.Reviews
            .Where(r => r.CoffeeShopId == coffeeShopId)
            .Select(r => new ReviewResponseDto(r.Id, r.UserName, r.Comment, r.Rating, DateTime.Now))
            .ToListAsync();
    }
}