using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeLocator.Domain.Interfaces;

/// <summary>
/// Coordinates the work of multiple repositories and ensures transaction integrity (Atomicity).
/// </summary>
public interface IUnitOfWork : IDisposable
{
  
    ICoffeeShopRepository CoffeeShops { get; }
    IVisitRepository Visits { get; }
    IAchievementRepository Achievements { get; }
    Task<int> SaveChangesAsync();
}