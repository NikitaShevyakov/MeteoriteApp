using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.Repositories;

namespace MeteoriteApp.Infrastructure.Services.Sync
{
    public class CategorySyncService : BaseSyncService<CategoryEntity>
    {
        public CategorySyncService(IRepository<CategoryEntity> repository)
            : base(repository, c => c.Name, name => new CategoryEntity { Name = name })
        { }
    }    
}
