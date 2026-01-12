using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeLocator.Domain.Entities;

namespace CoffeeLocator.Domain.Interfaces;

/// <summary>
/// Defines the data access contract for user Achievements.
/// </summary>
public interface IAchievementRepository
{
    Task<IEnumerable<Achievement>> GetUserAchievementsAsync(Guid userId);
    Task AddAsync(Achievement achievement);
}