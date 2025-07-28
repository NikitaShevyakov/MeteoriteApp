using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites.Configurations
{
    public class DiscoveryStatusEntityConfiguration
        : IEntityTypeConfiguration<DiscoveryStatusEntity>
    {
        public void Configure(EntityTypeBuilder<DiscoveryStatusEntity> builder)
        {
            builder.ToTable("T_MeteoriteDiscoveryStatuses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(d => d.Meteorites)
                .WithOne(m => m.DiscoveryStatus)
                .HasForeignKey(m => m.DiscoveryStatusId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
