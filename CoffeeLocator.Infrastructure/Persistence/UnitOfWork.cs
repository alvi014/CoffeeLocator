using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using CoffeeLocator.Infrastructure.Repositories;

namespace CoffeeLocator.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Campos privados para caché
        private ICoffeeShopRepository? _coffeeShops;
        private IVisitRepository? _visits;
        private IAchievementRepository? _achievements;
        private IUserRepository? _users;
        private IReviewRepository? _reviews;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Propiedades con inicialización perezosa (Lazy Loading)
        // Si es nula, la crea; si ya existe, devuelve la misma instancia.
        public ICoffeeShopRepository CoffeeShops => _coffeeShops ??= new CoffeeShopRepository(_context);
        public IVisitRepository Visits => _visits ??= new VisitRepository(_context);
        public IAchievementRepository Achievements => _achievements ??= new AchievementRepository(_context);
        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);

        /// <summary>
        /// Persists all changes tracked by the context to the database.
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}