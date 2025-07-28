using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites.Configurations
{
    public class ClassificationEntityConfiguration : IEntityTypeConfiguration<ClassificationEntity>
    {
        public void Configure(EntityTypeBuilder<ClassificationEntity> builder)
        {
            builder.ToTable("T_MeteoriteClassifications");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Meteorites)
                .WithOne(m => m.Classification)
                .HasForeignKey(m => m.ClassificationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Name)
                .IsUnique()
                .HasDatabaseName("IX_T_MeteoriteClassifications_Name");
        }
    }
}
