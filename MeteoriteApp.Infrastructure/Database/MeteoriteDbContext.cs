using Microsoft.EntityFrameworkCore;

namespace MeteoriteApp.Infrastructure.Database
{
    public partial class MeteoriteDbContext : DbContext
    {        

        public MeteoriteDbContext(DbContextOptions<MeteoriteDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeteoriteDbContext).Assembly);
        }
    }
}