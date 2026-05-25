using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class LesRepository : ILesRepository
    {
        private readonly ApplicationDbContext _context;

        public LesRepository(ApplicationDbContext _context) { 
            this._context = _context;
        }

        public async Task<List<Les>> GetLessenByAanbodIdAsync(int aanbodId) { 
            return await _context.Lessen.Where(a => a.AanbodId == aanbodId).ToListAsync();

        }

        public async Task<Les> GetLesByIdAsync(int lesId) { 
            return await _context.Lessen.FirstOrDefaultAsync(l => l.Id == lesId);
        }
    }
}
