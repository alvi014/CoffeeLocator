using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a coffee shop establishment registered in the system.
/// </summary>
public class CoffeeShop : BaseEntity
{
    public string Name { get; private set; }
    public string GooglePlaceId { get; private set; }
    public string? Description { get; set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public bool IsPremium { get; private set; }
    public string? ImageUrl { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public double AverageRating => Reviews.Any() ? Math.Round(Reviews.Average(r => r.Rating), 1) : 0;
    public int TotalReviews => Reviews.Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoffeeShop"/> class.
    /// </summary>
    /// <param name="name">Name of the coffee shop.</param>
    /// <param name="googlePlaceId">Google Place identifier.</param>
    /// <param name="address">Physical address of the coffee shop.</param>
    /// <param name="latitude">Latitude coordinate.</param>
    /// <param name="longitude">Longitude coordinate.</param>
    /// <param name="description">Optional description.</param>
    /// <param name="isPremium">Indicates whether the shop is premium.</param>
    public CoffeeShop(string name, string googlePlaceId, string address, double latitude, double longitude, string? description = null, bool isPremium = false)
    {
        Name = name;
        GooglePlaceId = googlePlaceId;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        IsPremium = isPremium;
    }

    /// <summary>
    /// Metod for updating the premium status of the coffee shop.
    /// </summary>
    /// <param name="status"></param>
    public void SetPremiumStatus(bool status)
    {
        IsPremium = status;
    }
}