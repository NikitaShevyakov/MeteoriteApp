namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites
{
    public class MeteoriteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Mass { get; set; }
        public DateTime DateDiscovered { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string RawRegionByDistrict { get; set; }
        public string RawRegionByGeozone { get; set; }


        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public int ClassificationId { get; set; }
        public ClassificationEntity Classification { get; set; }
        public int DiscoveryStatusId { get; set; }
        public DiscoveryStatusEntity DiscoveryStatus { get; set; }
        public GeoLocationEntity GeoLocation { get; set; }
    }
}