using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a user review and rating for a specific coffee shop.
/// </summary>
public class Review : BaseEntity
{
    public string UserName { get; private set; }
    public string Comment { get; private set; }
    public int Rating { get; private set; } 
    public Guid CoffeeShopId { get; private set; }

    /// <summary>
    /// Builds a new Review instance.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="comment"></param>
    /// <param name="rating"></param>
    /// <param name="coffeeShopId"></param>
    public Review(string userName, string comment, int rating, Guid coffeeShopId)
    {
        UserName = userName;
        Comment = comment;
        Rating = rating;
        CoffeeShopId = coffeeShopId;
    }
}