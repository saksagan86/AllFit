using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Allfit_Webproject.Server.Dtos;

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
            try
            {
                var verhalen = await _service.GetVerhalenAsync(aanbodType);
                return Ok(verhalen);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVerhaalById(int id)
        {
            try
            {
                var verhaal = await _service.GetVerhaalByIdAsync(id);
                if (verhaal == null) return NotFound();
                return Ok(verhaal);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> VerhaalToevoegen([FromForm] VerhaalToevoegenDto dto)
        {
            var subClaim = User.FindFirst("sub")
                ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (subClaim == null)
                return BadRequest(new { message = "Geen sub claim gevonden" });

            var lidId = int.Parse(subClaim.Value);
            try
            {
                await _service.VerhaalToevoegenAsync(lidId, dto);
                return Ok(new { message = "Verhaal succesvol toegevoegd" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
