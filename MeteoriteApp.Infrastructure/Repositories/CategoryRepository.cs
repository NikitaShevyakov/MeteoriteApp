using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Repositories
{
    public class CategoryRepository 
        : BaseRepository<CategoryEntity>
    {
        public CategoryRepository(MeteoriteDbContext context)
            : base(context)
        {
        }
    }
}
