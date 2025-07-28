using Asp.Versioning;
using MeteoriteApp.Infrastructure.Services;
using MeteoriteApp.Infrastructure.Services.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MeteoriteApp.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MeteoritesController : BaseApiController
    {
        private readonly MeteoriteService _service;

        public MeteoritesController(MeteoriteService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct = default) 
            => await ModelOrErrorAsync(async () => await _service.GetAllAsync(ct));

        [HttpGet("years")]
        public async Task<IActionResult> GetYears(CancellationToken ct = default)
            => await ModelOrErrorAsync(async() => await _service.GetYearsAsync(ct));        

        [HttpGet("classifications")]
        public async Task<IActionResult> GetClassifications(CancellationToken ct = default)
            => await ModelOrErrorAsync(async () => await _service.GetClassificationsAsync(ct));

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] MeteoriteFilter query)
            => await ModelOrErrorAsync(async () => await _service.GetGroupedSummaryAsync(query));        
    }
}
