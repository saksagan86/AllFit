using Allfit_Webproject.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Allfit_Webproject.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AanbodController : Controller
    {

        [HttpGet]
        public IEnumerable<Aanbod> GetAanbod()
        {
            // Tijdelijke mock data: we simuleren wat de database straks gaat doen
            var groepsles = new Groepsles("Beginners", "45 minuten");

            // Je zou hier ook Fitness en Kickboks objecten aanmaken
            var fitness = new Fitness();
            var kickboks = new Kickboks();

            // We sturen een lijst terug met al het aanbod
            return new List<Aanbod> { groepsles, fitness, kickboks };
        }
    }
}
