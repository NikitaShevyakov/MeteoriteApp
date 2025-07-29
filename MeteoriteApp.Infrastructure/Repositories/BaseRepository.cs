using MeteoriteApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class BaseRepository<T> 
        : IRepository<T> where T : class
    {
        private readonly MeteoriteDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MeteoriteDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
            => await _dbSet
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task AddAsync(List<T> toAdd,CancellationToken ct = default)
        {
            if (toAdd == null || toAdd.Count == 0)
                return;

            await _dbSet.AddRangeAsync(toAdd, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task RemoveAsync(List<T> toRemove,CancellationToken ct = default)
        {
            if (toRemove == null || toRemove.Count == 0)
                return;

            _dbSet.RemoveRange(toRemove);
            await _context.SaveChangesAsync(ct);
        }
    }
}
