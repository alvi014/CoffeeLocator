using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;

namespace CoffeeLocator.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Properties defined in your interface
        public ICoffeeShopRepository CoffeeShops { get; }
        public IVisitRepository Visits { get; }
        public IAchievementRepository Achievements { get; }

        public UnitOfWork(
            AppDbContext context,
            ICoffeeShopRepository coffeeShops,
            IVisitRepository visits,
            IAchievementRepository achievements)
        {
            _context = context;
            CoffeeShops = coffeeShops;
            Visits = visits;
            Achievements = achievements;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}