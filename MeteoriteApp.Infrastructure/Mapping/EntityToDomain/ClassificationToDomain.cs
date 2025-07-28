using MeteoriteApp.Domain.Models;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Mapping.EntityToDomain
{
    public static class ClassificationToDomain
    {
        public static Classification ToDomain(this ClassificationEntity entity)
            => new Classification(entity.Id, entity.Name);
        
    }
}
