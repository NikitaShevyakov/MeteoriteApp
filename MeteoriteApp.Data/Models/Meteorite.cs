namespace MeteoriteApp.Domain.Models
{
    public class Meteorite
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Mass { get; set; }
        public DateTime? DateDiscovered { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Category Category { get; set; }
        public Classification Classification { get; set; }
        public DiscoveryStatus DiscoveryStatus { get; set; }
        public GeoLocation GeoLocation { get; set; }
        public string RawRegionByDistrict { get; set; }
        public string RawRegionByGeozone { get; set; }
    }
}
