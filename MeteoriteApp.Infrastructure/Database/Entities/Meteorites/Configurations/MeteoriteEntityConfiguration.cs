using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites.Configurations
{
    public class MeteoriteEntityConfiguration
        : IEntityTypeConfiguration<MeteoriteEntity>
    {
        public void Configure(EntityTypeBuilder<MeteoriteEntity> builder)
        {
            builder.ToTable("T_Meteorites");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Mass).IsRequired();
            builder.Property(m => m.DateDiscovered).IsRequired();
            builder.Property(m => m.Latitude).IsRequired();
            builder.Property(m => m.Longitude).IsRequired();

            builder.Property(m => m.RawRegionByDistrict)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(m => m.RawRegionByGeozone)
                .HasMaxLength(100)
                .IsRequired(false);

            builder
                .HasOne(m => m.Category)
                .WithMany(c => c.Meteorites)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.Classification)
                .WithMany(c => c.Meteorites)
                .HasForeignKey(m => m.ClassificationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.DiscoveryStatus)
                .WithMany(s => s.Meteorites)
                .HasForeignKey(m => m.DiscoveryStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.GeoLocation)
                .WithOne(g => g.Meteorite)
                .HasForeignKey<GeoLocationEntity>(g => g.MeteoriteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.Name)
                .HasDatabaseName("IX_T_Meteorites_Name");
        }
    }
}