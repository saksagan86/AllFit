using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class LidRepository : ILidRepository
    {
        private readonly ApplicationDbContext _context;

        public LidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLidAsync(Lid lid)
        {
            _context.Lid.Add(lid);
            await _context.SaveChangesAsync();
        }

        public async Task<Lid?> GetByEmailAsync(string email)
        {
            return await _context.Lid.FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<Lidmaatschap?> GetLidmaatschapById(int lidmaatschapId) {
            return await _context.Lidmaatschap.FindAsync(lidmaatschapId);
        }
    }
}
