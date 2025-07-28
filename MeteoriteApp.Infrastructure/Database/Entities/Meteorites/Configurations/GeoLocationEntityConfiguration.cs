using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites.Configurations
{
    public class GeoLocationEntityConfiguration
        : IEntityTypeConfiguration<GeoLocationEntity>
    {
        public void Configure(EntityTypeBuilder<GeoLocationEntity> builder)
        {
            builder.ToTable("T_MeteoriteGeoLocations");
            builder.HasKey(gl => gl.MeteoriteId);

            builder.Property(g => g.Latitude)
               .IsRequired()
               .HasColumnType("float");

            builder.Property(g => g.Longitude)
                .IsRequired()
                .HasColumnType("float");            
           
            builder.Property(g => g.TypeId)
                .IsRequired();

            builder.HasOne(g => g.Meteorite)
                .WithOne(m => m.GeoLocation)
                .HasForeignKey<GeoLocationEntity>(g => g.MeteoriteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Type)
                .WithMany(t => t.GeoLocations)
                .HasForeignKey(g => g.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}