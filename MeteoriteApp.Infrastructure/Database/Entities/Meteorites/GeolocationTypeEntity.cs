namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites
{
    public class GeolocationTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GeoLocationEntity> GeoLocations { get; set; }
    }
}
