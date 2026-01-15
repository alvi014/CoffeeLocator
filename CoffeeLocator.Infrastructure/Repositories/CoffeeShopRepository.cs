using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Repositories;

public class CoffeeShopRepository : ICoffeeShopRepository
{
    private readonly AppDbContext _context;

    public CoffeeShopRepository(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Metod <see langword="for"/> getting all CoffeeShops including their reviews.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CoffeeShop>> GetAllWithReviewsAsync()
    {
        return await _context.CoffeeShops
            .Include(s => s.Reviews)
            .ToListAsync();
    }
    /// <summary>
    /// Metod <see langword="for"/> getting a CoffeeShop by its unique identifier, including its reviews.   
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<CoffeeShop?> GetByIdWithReviewsAsync(Guid id)
    {
        return await _context.CoffeeShops
            .Include(s => s.Reviews)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    /// <summary>
    /// Metod <see langword="for"/> getting a CoffeeShop by its unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<CoffeeShop?> GetByIdAsync(Guid id)
    {
        return await _context.CoffeeShops.FindAsync(id);
    }
    /// <summary>
    /// Metod <see langword="for"/> getting a CoffeeShop by its Google Place ID.
    /// </summary>
    /// <param name="googleId"></param>
    /// <returns></returns>
    public async Task<CoffeeShop?> GetByGoogleIdAsync(string googleId)
    {
        return await _context.CoffeeShops
            .FirstOrDefaultAsync(s => s.GooglePlaceId == googleId);
    }

    /// <summary>
    /// Metod <see langword="for"/> adding a new CoffeeShop to the database.    
    /// </summary>
    /// <param name="shop"></param>
    /// <returns></returns>
    public async Task AddAsync(CoffeeShop shop)
    {
        await _context.CoffeeShops.AddAsync(shop);
        await _context.SaveChangesAsync();
    }
}