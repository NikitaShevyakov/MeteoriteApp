using MeteoriteApp.Domain.Models;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Mapping.EntityToDomain
{
    public static class MeteoriteToDomain
    {
        public static Meteorite ToDomain(this MeteoriteEntity entity)
        {
            return new Meteorite
            {
                Id = entity.Id,
                Name = entity.Name,
                Mass = entity.Mass,
                DateDiscovered = entity.DateDiscovered,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Category = new Category(entity.Category.Id, entity.Category.Name),
                Classification = new Classification(entity.Classification.Id, entity.Classification.Name),
                DiscoveryStatus = new DiscoveryStatus(entity.DiscoveryStatus.Id, entity.DiscoveryStatus.Name),
                GeoLocation = entity.GeoLocation == null ? null : new GeoLocation()
                {
                    Latitude = entity.GeoLocation.Latitude,
                    Longitude = entity.GeoLocation.Longitude,
                    Type = new GeolocationType(entity.GeoLocation.Type.Id, entity.GeoLocation.Type.Name)
                },
                RawRegionByDistrict = entity.RawRegionByDistrict,
                RawRegionByGeozone = entity.RawRegionByGeozone
            };
        }
    }
}
