using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a coffee shop establishment registered in the system.
/// </summary>
public class CoffeeShop : BaseEntity
{
    public string Name { get; set; }
    public string? GooglePlaceId { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsPremium { get; set; }
    public string? ImageUrl { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public double AverageRating => Reviews.Any() ? Math.Round(Reviews.Average(r => r.Rating), 1) : 0;
    public int TotalReviews => Reviews.Count;


    public CoffeeShop() { }

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
    public CoffeeShop(string name, string? googlePlaceId, string address, double latitude, double longitude, string? description = null, bool isPremium = false)
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


    /// <summary>
    /// Metod for updating the information of the coffee shop.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="description"></param>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <param name="imageUrl"></param>
    public void UpdateInfo(string name, string address, string? description, double lat, double lon, string? imageUrl)
    {
        Name = name;
        Address = address;
        Description = description;
        Latitude = lat;
        Longitude = lon;
        ImageUrl = imageUrl;
    }


}