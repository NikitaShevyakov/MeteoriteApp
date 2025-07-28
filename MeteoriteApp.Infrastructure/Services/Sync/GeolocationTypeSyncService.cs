using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.Repositories;

namespace MeteoriteApp.Infrastructure.Services.Sync
{
    public class GeolocationTypeSyncService : BaseSyncService<GeolocationTypeEntity>
    {
        public GeolocationTypeSyncService(IRepository<GeolocationTypeEntity> repository)
            : base(repository, c => c.Name, type => new GeolocationTypeEntity { Name = type })
        { }
    }
}
