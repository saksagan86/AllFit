using Microsoft.AspNetCore.Mvc;
using Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportschoolController : ControllerBase
    {
        private readonly ISportschoolService _service;
    
        public SportschoolController(ISportschoolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSportscholen()
        {
            var sportscholen = await _service.ToonAlleSportscholenOpAsync();

            return Ok(sportscholen);
        }

        [HttpGet("navbar")]
        public async Task<IActionResult> GetNamenSportscholen()
        {
            var sportscholen = await _service.ToonAlleSportscholenOpAsync();

            var result = sportscholen.Select(s => new ToonSportschoolNaamDTO()
            {
                Id = s.Id,
                Naam = s.Naam,
            });

            return Ok(result);
        }

    }
}
