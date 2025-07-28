namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites
{
    public class GeoLocationEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int TypeId { get; set; }
        public GeolocationTypeEntity Type { get; set; }

        public int MeteoriteId { get; set; }
        public MeteoriteEntity Meteorite { get; set; }
    }
}