using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;

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
                await _service.RegisterLidAsync(dto);
                return Ok(new { message = "Lid aangemaakt" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}