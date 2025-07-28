using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class DiscoveryStatusRepository 
        : BaseRepository<DiscoveryStatusEntity>
    {
        public DiscoveryStatusRepository(MeteoriteDbContext context)
            : base(context)
        {
        }
    }
}
