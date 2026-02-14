using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;

namespace CoffeeLocator.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICoffeeShopRepository CoffeeShops { get; }
        public IVisitRepository Visits { get; }
        public IAchievementRepository Achievements { get; } 
        public IUserRepository Users { get; }
        public IReviewRepository Reviews { get; }

        /// <summary>
        /// Builder of inyection dependencys
        /// </summary>
        /// <param name="context"></param>
        /// <param name="coffeeShops"></param>
        /// <param name="visits"></param>
        /// <param name="achievements"></param>
        /// <param name="users"></param>
        /// <param name="reviews"></param>
        public UnitOfWork(
            AppDbContext context,
            ICoffeeShopRepository coffeeShops,
            IVisitRepository visits,
            IAchievementRepository achievements,
            IUserRepository users, 
            IReviewRepository reviews) 
        {
            _context = context;
            CoffeeShops = coffeeShops;
            Visits = visits;
            Achievements = achievements;
            Users = users;
            Reviews = reviews;
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