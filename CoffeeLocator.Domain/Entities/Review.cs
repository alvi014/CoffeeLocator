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


    public virtual CoffeeShop CoffeeShop { get; private set; }


    /// <summary>
    /// Build Professional
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="comment"></param>
    /// <param name="rating"></param>
    /// <param name="coffeeShopId"></param>
    public Review(Guid userId, string comment, int rating, Guid coffeeShopId)
    {
        Id = Guid.NewGuid(); 
        UserId = userId;
        Comment = comment;
        Rating = rating;
        CoffeeShopId = coffeeShopId;
    }
}