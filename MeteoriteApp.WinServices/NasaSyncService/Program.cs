using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.HttpClients;
using Microsoft.EntityFrameworkCore;
using NasaSyncService;
using NasaSyncService.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddDbContext<MeteoriteDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMeteoriteRepositories();
builder.Services.AddMeteoriteServices();

builder.Services.AddScoped<HttpUtils>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();