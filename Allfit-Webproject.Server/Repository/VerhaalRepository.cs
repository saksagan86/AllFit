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

            if (aanbodType == "Kickboks") query = query.Where(v => v.Aanbod is Kickboks);
            if (aanbodType == "Fitness") query = query.Where(v => v.Aanbod is Fitness);
            if (aanbodType == "Groepsles") query = query.Where(v => v.Aanbod is Groepsles);

            return await query.ToListAsync();
        }

        public async Task<Verhaal?> GetVerhaalByIdAsync(int id)
        {
            return await _context.Verhalen
                .Include(v => v.Lid)
                .Include(v => v.Aanbod)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task VerhaalToevoegenAsync(Verhaal verhaal)
        {
            await _context.Verhalen.AddAsync(verhaal);
            await _context.SaveChangesAsync();
        }

    }
}
