using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class SportschoolRepository : ISportschoolRepository
    {
        private readonly ApplicationDbContext _context;

        public SportschoolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sportschool>> GetAllSportscholenAsync() {
        
            return await _context.Sportscholen
                .Include(s => s.openingstijdenSportschool)
                .Include(s => s.alleFaciliteiten)
                .ToListAsync();
        
        }

        
    }
}
