using CoffeeLocator.Domain.Entities;

namespace CoffeeLocator.Domain.Interfaces;

/// <summary>
/// Defines a contract for managing and accessing review entities in a data store.
/// </summary>
/// <remarks>Implementations of this interface provide asynchronous and synchronous methods for retrieving,
/// adding, updating, and deleting reviews. Methods that return tasks are intended for use with asynchronous programming
/// models. Thread safety and transaction management are implementation-specific and should be considered when using
/// concrete implementations.</remarks>
public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetByCoffeeShopIdAsync(Guid coffeeShopId);
    Task AddAsync(Review review);
    void Update(Review review);
    void Delete(Review review);
}