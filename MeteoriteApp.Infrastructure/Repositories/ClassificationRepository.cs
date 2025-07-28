using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class ClassificationRepository 
        : BaseRepository<ClassificationEntity>
    {
        public ClassificationRepository(MeteoriteDbContext context)
            : base(context)
        {
        }
    }
}
