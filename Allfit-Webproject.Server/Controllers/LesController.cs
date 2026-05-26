using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LesController : ControllerBase
    {
        private readonly ILesService _service;

        public LesController(ILesService _service)
        {
            this._service = _service;

        }

        [Authorize]
        [HttpGet("{aanbodId}/lessen")]
        public async Task<IActionResult> GetLessenByAanbod(int aanbodId)
        {

            var subClaim = User.FindFirst("sub")
            ?? User.FindFirst(ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return BadRequest(new { message = "Geen sub claim gevonden" });
            var lidId = int.Parse(subClaim.Value);

            try
            {
                var lessen = await _service.GetLessenByAanbodAsync(aanbodId, lidId);
                return Ok(lessen);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
