using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.HttpClients;
using MeteoriteApp.Infrastructure.Repositories;
using MeteoriteApp.Infrastructure.Services;
using MeteoriteApp.Infrastructure.Services.Sync;

namespace MeteoriteApp.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMeteoriteServices(this IServiceCollection services)
        {
            services.AddScoped<ISyncService<CategoryEntity>>(provider =>
                new BaseSyncService<CategoryEntity>(
                    provider.GetRequiredService<IRepository<CategoryEntity>>(),
                    c => c.Name,
                    name => new CategoryEntity { Name = name }
                ));

            services.AddScoped<ISyncService<ClassificationEntity>>(provider =>
                new BaseSyncService<ClassificationEntity>(
                    provider.GetRequiredService<IRepository<ClassificationEntity>>(),
                    c => c.Name,
                    name => new ClassificationEntity { Name = name }
                ));

            services.AddScoped<ISyncService<GeolocationTypeEntity>>(provider =>
                new BaseSyncService<GeolocationTypeEntity>(
                    provider.GetRequiredService<IRepository<GeolocationTypeEntity>>(),
                    g => g.Name,
                    name => new GeolocationTypeEntity { Name = name }
                ));

            services.AddScoped<ISyncService<DiscoveryStatusEntity>>(provider =>
                new BaseSyncService<DiscoveryStatusEntity>(
                    provider.GetRequiredService<IRepository<DiscoveryStatusEntity>>(),
                    d => d.Name,
                    name => new DiscoveryStatusEntity { Name = name }
                ));

            services.AddScoped<MeteoriteService>();

            services.AddScoped<HttpUtils>();

            return services;
        }
    }
}
