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
    /// Publishes a new review for a specific coffee shop.
    /// </summary>
    /// <remarks>
    /// This endpoint requires an active JWT token. The user identity is automatically extracted from the token claims.
    /// 
    /// **Validation Rules:**
    /// - Rating must be between 1 and 5.
    /// - Comment cannot be empty.
    /// </remarks>
    /// <param name="dto">The review details (Comment, Rating, and CoffeeShopId).</param>
    /// <response code="200">Review created successfully.</response>
    /// <response code="401">Unauthorized: Token is missing, expired, or invalid.</response>
    /// <response code="400">Bad Request: Validation errors in the input data.</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> PostReview(CreateReviewDto dto)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized("No se pudo identificar al usuario.");

        var userId = Guid.Parse(userIdClaim);

        var review = new Review(
            userId,
            dto.Comment,
            dto.Rating,
            dto.CoffeeShopId
        );

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Reseña publicada con éxito", ReviewId = review.Id });
    }

    /// <summary>
    /// Retrieves all reviews for a specific coffee shop.
    /// </summary>
    /// <remarks>
    /// Performs a JOIN operation with the Users table to return the full name of each reviewer.
    /// </remarks>
    /// <param name="coffeeShopId">The unique identifier (GUID) of the coffee shop.</param>
    /// <returns>A list of reviews with reviewer names, comments, and ratings.</returns>
    /// <response code="200">Returns the list of reviews.</response>
    /// <response code="404">If the coffee shop ID does not exist (returns empty list).</response>
    [HttpGet("shop/{coffeeShopId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetReviewsByShop(Guid coffeeShopId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.CoffeeShopId == coffeeShopId)
            .Join(_context.Users,
                review => review.UserId,
                user => user.Id,
                (review, user) => new ReviewResponseDto(
                    review.Id,
                    user.FullName,
                    review.Comment,
                    review.Rating,
                    DateTime.Now
                ))
            .ToListAsync();

        return Ok(reviews);
    }
}