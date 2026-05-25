using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedenController : ControllerBase
    {
        private readonly ILidService _service;

        public LedenController(ILidService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterLid([FromBody] RegisterLidDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int lidId = await _service.RegisterLidAsync(dto);
                return Ok(new { message = "Lid aangemaakt", id = lidId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("account")]
        public async Task<IActionResult> GetLidById()
        {
            var subClaim = User.FindFirst("sub")
                ?? User.FindFirst(ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return BadRequest(new { message = "Geen sub claim gevonden" });

            var lidId = int.Parse(subClaim.Value);
            try
            {
                GegevensLidDto lid = await _service.GetLidByIdAsync(lidId);
                if (lid == null)
                    return NotFound();
                return Ok(lid);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateLidById([FromBody] UpdateLidDto dto) {

            var subClaim = User.FindFirst("sub")
                ?? User.FindFirst(ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return BadRequest(new { message = "Geen sub claim gevonden" });

            var lidId = int.Parse(subClaim.Value);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateLidAsync(lidId, dto);
                return Ok(new { message = "Gegevens succesvol bijgewerkt" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}