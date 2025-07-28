using MeteoriteApp.Domain.Models;
using MeteoriteApp.Infrastructure.External.DTO;

namespace MeteoriteApp.Infrastructure.Mapping.ExternalToDomain
{
    public static class ExternalMeteoriteExtensions
    {
        public static Meteorite ToDomain(this ExternalMeteoriteDto dto,
            Dictionary<string, Category> categoryDic,
            Dictionary<string, Classification> classificationDic,
            Dictionary<string, DiscoveryStatus> statusDic,
            Dictionary<string, GeolocationType> geoTypeDic)
        {
            var category = categoryDic.GetValueOrDefault(dto.Category);
            var classification = classificationDic.GetValueOrDefault(dto.ClassificationCode);
            var status = statusDic.GetValueOrDefault(dto.DiscoveryStatus);
            var geoType = dto.GeoLocation?.Type == null ? null : geoTypeDic.GetValueOrDefault(dto.GeoLocation?.Type);

            return new Meteorite
            {
                Id = int.TryParse(dto.Id, out var id) ? id : 0,
                Name = dto.Name,
                Mass = double.TryParse(dto.Mass, out var mass) ? mass : null,
                DateDiscovered = DateTime.TryParse(dto.DateDiscovered, out var date) ? date : null,
                Latitude = double.TryParse(dto.Latitude, out var lat) ? lat : null,
                Longitude = double.TryParse(dto.Longitude, out var lon) ? lon : null,
                Category = category is not null ? category : null,
                Classification = classification is not null ? classification : null,
                DiscoveryStatus = status is not null ? status : null,
                GeoLocation = dto.GeoLocation is not null
                    ? new GeoLocation
                    {
                        Longitude = dto.GeoLocation.Longitude,
                        Latitude = dto.GeoLocation.Latitude,
                        Type = geoType is not null ? geoType : null
                    }
                    : null,

                RawRegionByDistrict = dto.RawRegionByDistrict,
                RawRegionByGeozone = dto.RawRegionByGeozone
            };
        }
    }
}
