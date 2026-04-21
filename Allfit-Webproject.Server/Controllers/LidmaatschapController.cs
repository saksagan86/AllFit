using Allfit_Webproject.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LidmaatschapController : ControllerBase
    {
        private static List<Lidmaatschap> _lidmaatschappen = new List<Lidmaatschap>
        {
            new Lidmaatschap { Id = 1, Naam = "Stille Lidmaatschap", Type = "stil", Inschrijfgeld = 19.99, Lidmaatschapgeld = 39.99, Duur = "1 maand", Opzegtermijn = "1 maand", Beschrijving = "Geen toegang tussen 17:00 en 20:00 uur" },
            new Lidmaatschap { Id = 2, Naam = "All-Day Lidmaatschap", Type = "normaal", Inschrijfgeld = 19.99, Lidmaatschapgeld = 49.99, Duur = "1 maand", Opzegtermijn = "1 maand", Beschrijving = "Iedere dag toegang tot onze club" },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Lidmaatschap>> GetAlleLidmaatschappen()
        {
            return Ok(_lidmaatschappen);
        }

        [HttpPut("{id}/prijs")]
        public ActionResult UpdatePrijs(int id, [FromBody] double nieuwePrijs)
        {

            var lidmaatschap = _lidmaatschappen.FirstOrDefault(l => l.Id == id);

            if (lidmaatschap == null)
            {
                return NotFound($"Lidmaatschap met ID {id} is niet gevonden.");
            }

            lidmaatschap.Lidmaatschapgeld = nieuwePrijs;

            return Ok(new
            {
                Message = "Prijs is succesvol gewijzigd!",
                GeupdateLidmaatschap = lidmaatschap
            });
        }
    }
}