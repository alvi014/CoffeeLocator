using CoffeeLocator.Application.DTOs.Reviews;

namespace CoffeeLocator.Application.DTOs.CoffeeShops;

public class CoffeeShopDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? GooglePlaceId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsPremium { get; set; }
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public List<ReviewResponseDto> RecentReviews { get; set; } = new();
    public string? ImageUrl { get; set; }
}