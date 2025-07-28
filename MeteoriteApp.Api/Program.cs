using Asp.Versioning;
using MeteoriteApp.Infrastructure.Database;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.HttpClients;
using MeteoriteApp.Infrastructure.Repositories;
using MeteoriteApp.Infrastructure.Services;
using MeteoriteApp.Infrastructure.Services.Sync;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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


builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})

.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Meteorites API",
        Version = "v1",
        Description = "API for getting meteorites information"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meteorites API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
