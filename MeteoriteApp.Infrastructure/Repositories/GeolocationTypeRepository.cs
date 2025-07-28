using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using Microsoft.EntityFrameworkCore;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class GeolocationTypeRepository 
        : BaseRepository<GeolocationTypeEntity>
    {
        public GeolocationTypeRepository(MeteoriteDbContext context)
            : base(context)
        {
        }
    }
}
