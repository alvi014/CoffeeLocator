using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a coffee shop establishment registered in the system.
/// </summary>
public class CoffeeShop : BaseEntity
{
    public string Name { get; private set; }
    public string GooglePlaceId { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public bool IsPremium { get; private set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public double AverageRating => Reviews.Any() ? Math.Round(Reviews.Average(r => r.Rating), 1) : 0;
    public int TotalReviews => Reviews.Count;

    /// <summary>
    /// Builder profesional para CoffeeShop
    /// </summary>
    /// <param name="name"></param>
    /// <param name="googlePlaceId"></param>
    /// <param name="address"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public CoffeeShop(string name, string googlePlaceId, string address, double latitude, double longitude)
    {
        Id = Guid.NewGuid(); 
        Name = name;
        GooglePlaceId = googlePlaceId;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        IsPremium = false;
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