using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a suggestion or feedback left by a user regarding a coffee shop's service.
/// </summary>
public class Comment : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid CoffeeShopId { get; private set; }
    public string Content { get; private set; }
    public bool IsReadByAdmin { get; private set; }

    /// <summary>
    /// Professional constructor for Comment.
    /// </summary>
    /// <param name="userId">The unique identifier of the user providing feedback.</param>
    /// <param name="coffeeShopId">The unique identifier of the coffee shop.</param>
    /// <param name="content">The suggestion or comment text.</param>
    public Comment(Guid userId, Guid coffeeShopId, string content)
    {
        UserId = userId;
        CoffeeShopId = coffeeShopId;
        Content = content;
        IsReadByAdmin = false;
    }

    /// <summary>
    /// Marks the comment as reviewed by an administrator.
    /// </summary>
    public void MarkAsRead()
    {
        IsReadByAdmin = true;
    }
}
