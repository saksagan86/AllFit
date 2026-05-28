using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("ContactFormulierPolicy")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> VerstuurContactFormulier([FromBody] ContactFormulierCreateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Website))
            {
                return BadRequest(new
                {
                    message = "Spamcontrole mislukt."
                });
            }

            var result = await _contactService.MaakContactBerichtAsync(dto);
            return Ok(result);
        }
    }
}