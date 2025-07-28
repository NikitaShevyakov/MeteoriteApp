using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MeteoriteApp.Infrastructure.Database
{
    public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<MeteoriteDbContext>
    {
        public MeteoriteDbContext CreateDbContext(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "MeteoriteApp.Infrastructure", "Database");
            Console.WriteLine(path);
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MeteoriteDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new MeteoriteDbContext(optionsBuilder.Options);
        }
    }
}