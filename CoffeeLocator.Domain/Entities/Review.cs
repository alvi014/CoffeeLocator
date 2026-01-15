using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a user review and rating for a specific coffee shop.
/// </summary>
public class Review : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Comment { get; private set; }
    public int Rating { get; private set; }
    public Guid CoffeeShopId { get; private set; }

    /// <summary>
    /// Builds a new Review instance.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="comment"></param>
    /// <param name="rating"></param>
    /// <param name="coffeeShopId"></param>
    public Review(Guid userId, string comment, int rating, Guid coffeeShopId)
    {
        UserId = userId;
        Comment = comment;
        Rating = rating;
        CoffeeShopId = coffeeShopId;
    }
}