using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class VerhaalRepository : IVerhaalRepository
    {

        private readonly ApplicationDbContext _context;

        public VerhaalRepository(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<List<Verhaal>> GetVerhalenAsync(string? aanbodType) {
            var query = _context.Verhalen
                .Include(v => v.Lid)
                .Include(v => v.Aanbod)
                .AsQueryable();

            if (aanbodType == "kickboksen") query = query.Where(v => v.Aanbod is Kickboks);
            if (aanbodType == "fitness") query = query.Where(v => v.Aanbod is Fitness);
            if (aanbodType == "groepsles") query = query.Where(v => v.Aanbod is Groepsles);

            return await query.ToListAsync();
        }

    }
}
