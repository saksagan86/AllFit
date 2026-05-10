using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class AanbodRepository : IAanbodRepository
    {
        private readonly ApplicationDbContext _context;

        public AanbodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Fitness>> GetFitnessAsync(int sportschoolId)
        {
            return await _context.Aanbod
                .OfType<Fitness>()
                .Where(a => a.SportschoolId == sportschoolId)
                .ToListAsync();
        }

        public async Task<List<Groepsles>> GetGroepslesAsync(int sportschoolId)
        {
            return await _context.Aanbod
                .OfType<Groepsles>()
                .Where(a => a.SportschoolId == sportschoolId)
                .ToListAsync();
        }

        public async Task<List<Kickboks>> GetKickboksenAsync(int sportschoolId)
        {
            return await _context.Aanbod
                .OfType<Kickboks>()
                .Where(a => a.SportschoolId == sportschoolId)
                .ToListAsync();
        }
    }
}