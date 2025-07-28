using EFCore.BulkExtensions;
using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using Microsoft.EntityFrameworkCore;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class MeteoriteRepository
        : BaseRepository<MeteoriteEntity>
    {
        private readonly MeteoriteDbContext _context;

        public MeteoriteRepository(MeteoriteDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<MeteoriteEntity> Get()
            => _context.Meteorites;

        public new async Task<List<MeteoriteEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Meteorites
                .Include(m => m.GeoLocation)
                    .ThenInclude(g => g.Type)
                .Include(m => m.DiscoveryStatus)
                .Include(m => m.Classification)
                .Include(m => m.Category)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        public async Task BulkSyncMeteoritesAsync(
            IList<MeteoriteEntity> incomingMeteorites,
            CancellationToken ct = default)
        {
            var existingIds = await _context.Meteorites
                .AsNoTracking()
                .Select(m => m.Id)
                .ToListAsync(ct);

            var incomingIds = incomingMeteorites.Select(m => m.Id).ToList();
            var toDeleteIds = existingIds.Except(incomingIds).ToList();

            var toDeleteMeteorites = toDeleteIds
                .Select(id => new MeteoriteEntity { Id = id })
                .ToList();

            var toUpsertMeteorites = incomingMeteorites
                .Select(m => new MeteoriteEntity
                {
                    Id = m.Id,
                    Name = m.Name,
                    Mass = m.Mass,
                    DateDiscovered = m.DateDiscovered,
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    RawRegionByDistrict = m.RawRegionByDistrict,
                    RawRegionByGeozone = m.RawRegionByGeozone,
                    CategoryId = m.CategoryId,
                    ClassificationId = m.ClassificationId,
                    DiscoveryStatusId = m.DiscoveryStatusId
                })
                .ToList();

            var toUpsertGeoLocations = incomingMeteorites
                .Where(m => m.GeoLocation != null)
                .Select(m => new GeoLocationEntity
                {
                    MeteoriteId = m.Id,
                    Latitude = m.GeoLocation.Latitude,
                    Longitude = m.GeoLocation.Longitude,
                    TypeId = m.GeoLocation.TypeId
                })
                .ToList();

            using var tx = await _context.Database.BeginTransactionAsync(ct);

            if (toDeleteMeteorites.Any())
            {
                await _context.BulkDeleteAsync(toDeleteMeteorites, cancellationToken: ct);
            }

            await _context.BulkInsertOrUpdateAsync(
                toUpsertMeteorites,
                new BulkConfig { SetOutputIdentity = false },
                cancellationToken: ct);

            await _context.BulkInsertOrUpdateAsync(
                toUpsertGeoLocations,
                new BulkConfig { SetOutputIdentity = false },
                cancellationToken: ct);

            await tx.CommitAsync(ct);
        }

        public new async Task<List<DateTime>> GetYearsAsync(CancellationToken cancellationToken = default)
            => await _context.Meteorites
                .AsNoTracking()
                .Select(x => x.DateDiscovered)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync(cancellationToken);

        public new async Task<List<ClassificationEntity>> GetClassificationsAsync(CancellationToken cancellationToken = default)
            => await _context.MeteoriteClassifications
                .AsNoTracking()
                .ToListAsync(cancellationToken);   
        

    }
}