using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/community")]
    public class CommunityController : ControllerBase
    {
        private readonly IVerhaalService _service;

        public CommunityController(IVerhaalService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetVerhalen([FromQuery] string? aanbodType)
        {
            var verhalen = await _service.GetVerhalenAsync(aanbodType);
            return Ok(verhalen);
        }
    }
}
