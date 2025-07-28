using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.Repositories;

namespace MeteoriteApp.Infrastructure.Services.Sync
{
    public class DiscoveryStatusSyncService : BaseSyncService<DiscoveryStatusEntity>
    {
        public DiscoveryStatusSyncService(IRepository<DiscoveryStatusEntity> repository)
            : base(repository, c => c.Name, ds => new DiscoveryStatusEntity { Name = ds })
        { }
    }
}
