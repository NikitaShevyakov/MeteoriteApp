using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.HttpClients;
using MeteoriteApp.Infrastructure.Repositories;
using MeteoriteApp.Infrastructure.Services;
using MeteoriteApp.Infrastructure.Services.Sync;
using Microsoft.EntityFrameworkCore;
using NasaSyncService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddDbContext<MeteoriteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<CategoryEntity>, CategoryRepository>();
builder.Services.AddScoped<IRepository<ClassificationEntity>, ClassificationRepository>();
builder.Services.AddScoped<IRepository<GeolocationTypeEntity>, GeolocationTypeRepository>();
builder.Services.AddScoped<IRepository<DiscoveryStatusEntity>, DiscoveryStatusRepository>();
builder.Services.AddScoped<MeteoriteRepository>();

builder.Services.AddScoped<CategorySyncService>();
builder.Services.AddScoped<ClassificationSyncService>();
builder.Services.AddScoped<GeolocationTypeSyncService>();
builder.Services.AddScoped<DiscoveryStatusSyncService>();
builder.Services.AddScoped<MeteoriteService>();

builder.Services.AddScoped<HttpUtils>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();