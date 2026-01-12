using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeLocator.Domain.Entities;

namespace CoffeeLocator.Domain.Interfaces;

/// <summary>
/// Defines the data access contract for Visit entities.
/// </summary>
public interface IVisitRepository
{

    Task<IEnumerable<Visit>> GetByUserIdAsync(Guid userId);
    Task<bool> HasUserVisitedAsync(Guid userId, Guid coffeeShopId);
    Task AddAsync(Visit visit);
}