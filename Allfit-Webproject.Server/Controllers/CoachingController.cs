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
    public class CoachingController : ControllerBase
    {
        private readonly ICoachingService _coachingService;

        public CoachingController(ICoachingService coachingService)
        {
            _coachingService = coachingService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var gebruikerId = GetIngelogdeGebruikerId();

            if (gebruikerId == null)
            {
                return Unauthorized(new { message = "Gebruiker kon niet uit de token worden gehaald." });
            }

            var result = await _coachingService.GetDashboardAsync(gebruikerId.Value);
            return Ok(result);
        }

        [HttpPost("kies-doel")]
        public async Task<IActionResult> KiesDoel([FromBody] KiesCoachingDoelDto dto)
        {
            var gebruikerId = GetIngelogdeGebruikerId();

            if (gebruikerId == null)
            {
                return Unauthorized(new { message = "Gebruiker kon niet uit de token worden gehaald." });
            }

            var result = await _coachingService.KiesDoelAsync(gebruikerId.Value, dto);

            if (result == null)
            {
                return BadRequest(new { message = "Ongeldig doel gekozen." });
            }

            return Ok(result);
        }

        [HttpPost("training-afronden")]
        public async Task<IActionResult> RondTrainingAf([FromBody] TrainingAfrondenDto dto)
        {
            var gebruikerId = GetIngelogdeGebruikerId();

            if (gebruikerId == null)
            {
                return Unauthorized(new { message = "Gebruiker kon niet uit de token worden gehaald." });
            }

            var result = await _coachingService.RondTrainingAfAsync(gebruikerId.Value, dto);

            if (result == null)
            {
                return BadRequest(new { message = "Training kon niet worden opgeslagen." });
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