using MeteoriteApp.Domain.Models;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;

namespace MeteoriteApp.Infrastructure.Mapping.DomainToEntity
{
    public static class MeteoriteToEntity
    {
        public static MeteoriteEntity ToEntity(this Meteorite meteorite) =>
            new MeteoriteEntity
            {
                Id = meteorite.Id,
                Name = meteorite.Name,
                Mass = meteorite.Mass.GetValueOrDefault(),
                DateDiscovered = meteorite.DateDiscovered.GetValueOrDefault(),
                Latitude = meteorite.Latitude.GetValueOrDefault(),
                Longitude = meteorite.Longitude.GetValueOrDefault(),
                CategoryId = meteorite.Category?.Id ?? 0,
                ClassificationId = meteorite.Classification?.Id ?? 0,
                DiscoveryStatusId = meteorite.DiscoveryStatus?.Id ?? 0,
                GeoLocation = meteorite.GeoLocation == null
                    ? null
                    : new GeoLocationEntity()
                        {
                            Latitude = meteorite.GeoLocation.Latitude,
                            Longitude = meteorite.GeoLocation.Longitude,
                            TypeId = meteorite.GeoLocation.Type?.Id ?? 0,
                            MeteoriteId = meteorite.Id
                        },
                RawRegionByDistrict = meteorite.RawRegionByDistrict,
                RawRegionByGeozone = meteorite.RawRegionByGeozone
            };        
    }
}
