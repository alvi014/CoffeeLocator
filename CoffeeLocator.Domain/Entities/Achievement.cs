using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeLocator.Domain.Common;

namespace CoffeeLocator.Domain.Entities;

/// <summary>
/// Represents a reward or milestone earned by a user through their coffee explorations.
/// </summary>
public class Achievement : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }

    /// <summary>
    /// Professional constructor for Achievement.
    /// </summary>
    /// <param name="title">The name of the badge or achievement.</param>
    /// <param name="description">Explanation of how it was earned.</param>
    /// <param name="userId">The owner of the achievement.</param>
    public Achievement(string title, string description, Guid userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }
}
