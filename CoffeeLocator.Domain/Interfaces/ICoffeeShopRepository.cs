using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeLocator.Domain.Entities;

namespace CoffeeLocator.Domain.Interfaces;

/// <summary>
/// Defines the data access contract for CoffeeShop entities.
/// </summary>
public interface ICoffeeShopRepository
{
    Task<CoffeeShop?> GetByIdAsync(Guid id);
    Task<CoffeeShop?> GetByGoogleIdAsync(string googlePlaceId);
    Task AddAsync(CoffeeShop coffeeShop);
    Task<IEnumerable<CoffeeShop>> GetAllWithReviewsAsync();
    Task<CoffeeShop?> GetByIdWithReviewsAsync(Guid id);
    Task DeleteAsync(CoffeeShop coffeeShop);
}