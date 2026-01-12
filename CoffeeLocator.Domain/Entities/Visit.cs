using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a user's visit to a specific coffee shop and their feedback.
/// </summary>
public class Visit : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid CoffeeShopId { get; private set; }
    public DateTime? VisitDate { get; private set; }
    public int? Rating { get; private set; }
    public string Review { get; private set; } = string.Empty;
    public bool HasBeenVisited { get; private set; }

    /// <summary>
    /// Professional constructor for Visit.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="coffeeShopId">The unique identifier of the coffee shop.</param>
    public Visit(Guid userId, Guid coffeeShopId)
    {
        UserId = userId;
        CoffeeShopId = coffeeShopId;
        HasBeenVisited = false;
    }

    /// <summary>
    /// Completes the visit by providing a rating and a review.
    /// </summary>
    /// <param name="rating">Score between 1 and 5.</param>
    /// <param name="review">User's feedback text.</param>
    public void CompleteVisit(int rating, string review)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        Rating = rating;
        Review = review;
        VisitDate = DateTime.UtcNow;
        HasBeenVisited = true;
    }
}