using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class GeoLocationRepository 
        : BaseRepository<GeoLocationEntity>
    {
        public GeoLocationRepository(MeteoriteDbContext context)
            : base(context)
        {
        }
    }
}
