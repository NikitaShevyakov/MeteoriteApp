using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using Microsoft.EntityFrameworkCore;

namespace MeteoriteApp.Infrastructure.Database
{
    public partial class MeteoriteDbContext
    {
        public DbSet<MeteoriteEntity> Meteorites { get; set; }
        public DbSet<CategoryEntity> MeteoriteCategories { get; set; }
        public DbSet<ClassificationEntity> MeteoriteClassifications { get; set; }
        public DbSet<DiscoveryStatusEntity> MeteoriteDiscoveryStauses { get; set; }
        public DbSet<GeoLocationEntity> MeteoriteGeolocations { get; set; }
        public DbSet<GeolocationTypeEntity> MeteoriteGeolocationTypes { get; set; }
    }
}
