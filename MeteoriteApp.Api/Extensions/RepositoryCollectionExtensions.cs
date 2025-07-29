using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.Repositories;

namespace MeteoriteApp.Api.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddMeteoriteRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<CategoryEntity>, BaseRepository<CategoryEntity>>();
            services.AddScoped<IRepository<ClassificationEntity>, BaseRepository<ClassificationEntity>>();
            services.AddScoped<IRepository<GeolocationTypeEntity>, BaseRepository<GeolocationTypeEntity>>();
            services.AddScoped<IRepository<DiscoveryStatusEntity>, BaseRepository<DiscoveryStatusEntity>>();
            services.AddScoped<MeteoriteRepository>();
            return services;
        }
    }
}
