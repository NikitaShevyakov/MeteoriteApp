using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites.Configurations
{
    public class GeolocationTypeEntityConfiguration
        : IEntityTypeConfiguration<GeolocationTypeEntity>
    {
        public void Configure(EntityTypeBuilder<GeolocationTypeEntity> builder)
        {
            builder.ToTable("T_MeteoriteGeolocationTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(t => t.GeoLocations)
                .WithOne(g => g.Type)
                .HasForeignKey(g => g.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
