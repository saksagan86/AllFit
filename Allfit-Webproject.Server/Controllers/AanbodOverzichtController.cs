using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/aanbod")]
    public class AanbodOverzichtController : ControllerBase
    {
        private readonly IAanbodService _service;

        public AanbodOverzichtController(IAanbodService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlleAanbod()
        {
            try
            {
                var result = await _service.GetAlleAanbodAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
