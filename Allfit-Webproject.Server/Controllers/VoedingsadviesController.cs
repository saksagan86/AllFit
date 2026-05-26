using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VoedingsadviesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public VoedingsadviesController(ApplicationDbContext db)
        {
            _db = db;
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

            var beschikbareDoelen = await HaalBeschikbareDoelenOp();

            var gebruikerDoel = await _db.GebruikerDoelen
                .Include(gd => gd.doel)
                    .ThenInclude(d => d!.voedingsschemas)
                        .ThenInclude(v => v.regels)
                .FirstOrDefaultAsync(gd => gd.gebruikerId == gebruikerId.Value);

            if (gebruikerDoel == null)
            {
                return Ok(new
                {
                    heeftDoel = false,
                    beschikbareDoelen
                });
            }

            var schema = gebruikerDoel.doel?.voedingsschemas.FirstOrDefault();

            if (schema == null)
            {
                return Ok(new
                {
                    heeftDoel = false,
                    beschikbareDoelen,
                    message = "Er is nog geen voedingsschema gekoppeld aan jouw gekozen doel."
                });
            }

            return Ok(new
            {
                heeftDoel = true,
                beschikbareDoelen,
                doel = new
                {
                    gebruikerDoel.doel!.id,
                    gebruikerDoel.doel.naam,
                    gebruikerDoel.doel.beschrijving
                },
                schema = new
                {
                    schema.id,
                    schema.titel,
                    schema.beschrijving,
                    regels = schema.regels
                        .OrderBy(r => r.volgorde)
                        .Select(r => new
                        {
                            r.id,
                            r.maaltijdMoment,
                            r.advies,
                            r.volgorde
                        })
                }
            });
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

            var doelBestaat = await _db.Doelen
                .AnyAsync(d => d.id == dto.DoelId && d.actief);

            if (!doelBestaat)
            {
                return BadRequest(new
                {
                    message = "Ongeldig doel gekozen."
                });
            }

            var gebruikerDoel = await _db.GebruikerDoelen
                .FirstOrDefaultAsync(gd => gd.gebruikerId == gebruikerId.Value);

            if (gebruikerDoel == null)
            {
                gebruikerDoel = new GebruikerDoel
                {
                    gebruikerId = gebruikerId.Value,
                    doelId = dto.DoelId,
                    aangemaaktOp = DateTime.UtcNow,
                    gewijzigdOp = DateTime.UtcNow
                };

                _db.GebruikerDoelen.Add(gebruikerDoel);
            }
            else
            {
                gebruikerDoel.doelId = dto.DoelId;
                gebruikerDoel.gewijzigdOp = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();

            return await GetMijnSchema();
        }

        private async Task<List<object>> HaalBeschikbareDoelenOp()
        {
            return await _db.Doelen
                .Where(d => d.actief)
                .OrderBy(d => d.id)
                .Select(d => new
                {
                    d.id,
                    d.naam,
                    d.beschrijving
                })
                .Cast<object>()
                .ToListAsync();
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