using MeteoriteApp.Domain.Models;
using MeteoriteApp.Infrastructure.Database.Entities.Meteorites;
using MeteoriteApp.Infrastructure.External.DTO;
using MeteoriteApp.Infrastructure.HttpClients;
using MeteoriteApp.Infrastructure.Mapping.DomainToEntity;
using MeteoriteApp.Infrastructure.Mapping.EntityToDomain;
using MeteoriteApp.Infrastructure.Mapping.ExternalToDomain;
using MeteoriteApp.Infrastructure.Repositories;
using MeteoriteApp.Infrastructure.Services.Filters;
using MeteoriteApp.Infrastructure.Services.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MeteoriteApp.Infrastructure.Services
{
    public class MeteoriteService
    {
        private readonly MeteoriteRepository _meteoriteRepository;
        private readonly ILogger<MeteoriteService> _logger;
        private readonly HttpUtils _http;
        private readonly ISyncService<CategoryEntity> _categorySyncService;
        private readonly ISyncService<ClassificationEntity> _classificationSyncService;
        private readonly ISyncService<GeolocationTypeEntity> _geolocationTypeSyncService;
        private readonly ISyncService<DiscoveryStatusEntity> _discoveryStatusSyncService;

        private const string DataUrl = "https://raw.githubusercontent.com/biggiko/nasa-dataset/refs/heads/main/y77d-th95.json";

        public MeteoriteService(
            HttpUtils http,
            MeteoriteRepository meteoriteRepository,
            ILogger<MeteoriteService> logger,
            ISyncService<CategoryEntity> categorySyncService,
            ISyncService<ClassificationEntity> classificationSyncService,
            ISyncService<GeolocationTypeEntity> geolocationTypeSyncService,
            ISyncService<DiscoveryStatusEntity> discoveryStatusSyncService)
        {
            _http = http;
            _meteoriteRepository = meteoriteRepository;
            _logger = logger;
            _categorySyncService = categorySyncService;
            _classificationSyncService = classificationSyncService;
            _geolocationTypeSyncService = geolocationTypeSyncService;
            _discoveryStatusSyncService = discoveryStatusSyncService;
        }

        public async Task SyncAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Synchronization started at {Time}", DateTimeOffset.Now);

            try
            {
                var data = await _http.GetAsync<List<ExternalMeteoriteDto>>(DataUrl) ?? new List<ExternalMeteoriteDto>();
                if (data.Count > 0)
                {
                    await SyncInDbAsync(data, ct);                    
                    _logger.LogInformation("Synchronization completed. Processed: {Count}", data.Count);
                }
                else
                {
                    _logger.LogWarning("Data is empty or failed to load.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during data synchronization.");
            }
        }

        private async Task SyncInDbAsync(List<ExternalMeteoriteDto> data, CancellationToken ct = default)
        {
            var aggregateMap = new Dictionary<string, HashSet<string>>
            {
                ["Category"] = new(),
                ["Classification"] = new(),
                ["GeoLocationType"] = new(),
                ["DiscoveryStatus"] = new()
            };

            foreach (var item in data)
            {
                if (!string.IsNullOrWhiteSpace(item.Category))
                    aggregateMap["Category"].Add(item.Category);

                if (!string.IsNullOrWhiteSpace(item.ClassificationCode))
                    aggregateMap["Classification"].Add(item.ClassificationCode);

                if (!string.IsNullOrWhiteSpace(item.GeoLocation?.Type))
                    aggregateMap["GeoLocationType"].Add(item.GeoLocation.Type);

                if (!string.IsNullOrWhiteSpace(item.DiscoveryStatus))
                    aggregateMap["DiscoveryStatus"].Add(item.DiscoveryStatus);
            }

            var categoryDic = (await _categorySyncService
                .SyncAsync(aggregateMap["Category"], ct))
                .ToDictionary(key => key.Name, val => new Category(val.Id, val.Name));
            var classificationDic = (await _classificationSyncService
                .SyncAsync(aggregateMap["Classification"], ct))
                .ToDictionary(key => key.Name, val => new Classification(val.Id, val.Name));
            var geolocationTypeDic = (await _geolocationTypeSyncService
                .SyncAsync(aggregateMap["GeoLocationType"], ct))
                .ToDictionary(key => key.Name, val => new GeolocationType(val.Id, val.Name));
            var discoveryStatusDic = (await _discoveryStatusSyncService
                .SyncAsync(aggregateMap["DiscoveryStatus"], ct))
                .ToDictionary(key => key.Name, val => new DiscoveryStatus(val.Id, val.Name));           

            var meteoritesDomain = data
                .Select(dto => dto.ToDomain(categoryDic, classificationDic, discoveryStatusDic, geolocationTypeDic))
                .ToList();           

            var meteoriteEntity = meteoritesDomain
                .Select(x => x.ToEntity())
                .ToList();

            await _meteoriteRepository.BulkSyncMeteoritesAsync(meteoriteEntity, ct);            
        }

        public async Task<List<Meteorite>> GetAllAsync(CancellationToken ct = default)
            => (await _meteoriteRepository.GetAllAsync(ct))
                .Select(x => x.ToDomain())
                .ToList();

        public async Task<List<DateTime>> GetYearsAsync(CancellationToken ct = default)
            => await _meteoriteRepository.GetYearsAsync(ct);

        public async Task<List<Classification>> GetClassificationsAsync(CancellationToken ct = default)
            => (await _meteoriteRepository.GetClassificationsAsync(ct))
            .Select(x => x.ToDomain())
            .ToList();

        public async Task<PagedResult<MeteoriteSummaryDto>> GetGroupedSummaryAsync(MeteoriteFilter f)
        {
            var query = _meteoriteRepository.Get()
                .Include(c => c.Classification)
                .AsNoTracking();

            if (f.FromYear.HasValue)
                query = query.Where(m => m.DateDiscovered.Year >= f.FromYear.Value);
            if (f.ToYear.HasValue)
                query = query.Where(m => m.DateDiscovered.Year <= f.ToYear.Value);
            if (f.ClassificationCode.HasValue)
                query = query.Where(m => m.Classification.Id == f.ClassificationCode);
            if (!string.IsNullOrEmpty(f.NameContains))
                query = query.Where(m => m.Name.Contains(f.NameContains));

            var groupedQuery = query
                .GroupBy(m => m.DateDiscovered)
                .Select(g => new MeteoriteSummaryDto
                {
                    Date = g.Key,
                    Count = g.Count(),
                    Mass = g.Sum(x => x.Mass)
                });

            var total = await groupedQuery.CountAsync();            

            groupedQuery = f.SortBy.ToLower() switch
            {
                "count" => f.SortOrder == "desc"
                    ? groupedQuery.OrderByDescending(x => x.Count)
                    : groupedQuery.OrderBy(x => x.Count),
                "mass" => f.SortOrder == "desc"
                    ? groupedQuery.OrderByDescending(x => x.Mass)
                    : groupedQuery.OrderBy(x => x.Mass),
                _ => f.SortOrder == "desc"
                    ? groupedQuery.OrderByDescending(x => x.Date)
                    : groupedQuery.OrderBy(x => x.Date)
            };

            var data = await groupedQuery
                .Skip((f.Page - 1) * f.Limit)
                .Take(f.Limit)
                .ToListAsync();

            return new PagedResult<MeteoriteSummaryDto>
            {
                Data = data,
                Total = total,
                Page = f.Page,
                Limit = f.Limit
            };
        }
    }
}
