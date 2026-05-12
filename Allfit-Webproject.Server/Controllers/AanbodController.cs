using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Allfit_Webproject.Server.Controllers
{

    [ApiController]
    [Route("api/sportscholen/{sportschoolId}/aanbod")]
    public class AanbodController : ControllerBase
    {
        private readonly IAanbodService _service;

        public AanbodController(IAanbodService service) {

            _service = service;
        }

        [HttpGet("fitness")]
        public async Task<IActionResult> GetFitness(int sportschoolId)
        {
            var result = await _service.GetFitnessAsync(sportschoolId);
            return Ok(result);
        }

        [HttpGet("groepslessen")]
        public async Task<IActionResult> GetGroepsles(int sportschoolId)
        {
            var result = await _service.GetGroepslesAsync(sportschoolId);
            return Ok(result);
        }

        [HttpGet("kickboksen")]
        public async Task<IActionResult> GetKickboksen(int sportschoolId)
        {
            var result = await _service.GetKickboksenAsync(sportschoolId);
            return Ok(result);
        }
    }
}
