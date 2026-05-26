using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InschrijvingController : ControllerBase
    {

        private readonly IInschrijvingService _service;

        public InschrijvingController(IInschrijvingService _service) {
            this._service = _service;
        
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddInschrijven([FromBody] InschrijfDto dto) {

            var subClaim = User.FindFirst("sub")
                ?? User.FindFirst(ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return BadRequest(new { message = "Geen sub claim gevonden" });
            var lidId = int.Parse(subClaim.Value);

            try
            {
                await _service.AddInschrijvingenAsync(dto, lidId);
                return Ok(new { message = "Inschrijving succesvol" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


    }
}
