using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VoedingsadviesController : ControllerBase
    {
        private readonly IVoedingsadviesService _voedingsadviesService;

        public VoedingsadviesController(IVoedingsadviesService voedingsadviesService)
        {
            _voedingsadviesService = voedingsadviesService;
        }

        [HttpGet("mijn-schema")]
        public async Task<IActionResult> GetMijnSchema()
        {
            var gebruikerId = GetIngelogdeGebruikerId();

            if (gebruikerId == null)
            {
                return Unauthorized(new
                {
                    message = "Gebruiker kon niet uit de token worden gehaald."
                });
            }

            var result = await _voedingsadviesService.GetMijnSchemaAsync(gebruikerId.Value);
            return Ok(result);
        }

        [HttpPost("kies-doel")]
        public async Task<IActionResult> KiesDoel([FromBody] KiesVoedingsDoelDto dto)
        {
            var gebruikerId = GetIngelogdeGebruikerId();

            if (gebruikerId == null)
            {
                return Unauthorized(new
                {
                    message = "Gebruiker kon niet uit de token worden gehaald."
                });
            }

            var result = await _voedingsadviesService.KiesDoelAsync(gebruikerId.Value, dto);

            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Ongeldig doel gekozen."
                });
            }

            return Ok(result);
        }

        private int? GetIngelogdeGebruikerId()
        {
            var claimValue =
                User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub");

            if (!int.TryParse(claimValue, out var gebruikerId))
            {
                return null;
            }

            return gebruikerId;
        }
    }
}