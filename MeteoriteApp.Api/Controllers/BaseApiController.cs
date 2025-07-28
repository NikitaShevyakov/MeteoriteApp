using Microsoft.AspNetCore.Mvc;

namespace MeteoriteApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected async Task<IActionResult> ModelOrErrorAsync<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { error = ex.Message }
                );
            }
        }
    }
}
