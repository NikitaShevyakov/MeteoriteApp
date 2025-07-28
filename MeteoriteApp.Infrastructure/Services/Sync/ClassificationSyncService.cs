using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.Repositories;

namespace MeteoriteApp.Infrastructure.Services.Sync
{
    public class ClassificationSyncService 
        : BaseSyncService<ClassificationEntity>
    {
        public ClassificationSyncService(IRepository<ClassificationEntity> repository)
            : base(repository, c => c.Name, code => new ClassificationEntity { Name = code })
        { }
    }
}
