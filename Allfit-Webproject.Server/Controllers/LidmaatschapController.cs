using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllfitWebproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LidmaatschappenController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public LidmaatschappenController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.Lidmaatschap.AsNoTracking().ToListAsync();
            return Ok(items);
        }

    }
}