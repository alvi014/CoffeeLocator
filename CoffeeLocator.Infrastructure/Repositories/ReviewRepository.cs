using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Repositories;

/// <summary>
/// Functionality for managing reviews in the database
/// </summary>
public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Review?> GetByIdAsync(Guid id) =>
        await _context.Reviews.FindAsync(id);

    public async Task<IEnumerable<Review>> GetByCoffeeShopIdAsync(Guid coffeeShopId) =>
        await _context.Reviews
            .Where(r => r.CoffeeShopId == coffeeShopId)
            .ToListAsync();

    public async Task AddAsync(Review review) =>
        await _context.Reviews.AddAsync(review);

    public void Update(Review review) =>
        _context.Reviews.Update(review);

    public void Delete(Review review) =>
        _context.Reviews.Remove(review);
}
