using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Allfit_Webproject.Server.Repository
{
    public class LidRepository : ILidRepository
    {
        private readonly ApplicationDbContext _context;

        public LidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddLidAsync(Lid lid)
        {
            _context.Lid.Add(lid);
            await _context.SaveChangesAsync();
            return lid.id;
        }

        public async Task<Lid?> GetByEmailAsync(string email)
        {
            return await _context.Lid.FirstOrDefaultAsync(x => x.email == email);
        }
        
        public async Task<Lidmaatschap?> GetLidmaatschapById(int lidmaatschapId) {
            return await _context.Lidmaatschap.FindAsync(lidmaatschapId);
        }

        public async Task<Lid?> GetLidByIdAsync(int lidId) {
            return await _context.Lid.FirstOrDefaultAsync(l => l.id == lidId);
        }

        public async Task UpdateLidAsync(Lid updatedLid) {
            _context.Lid.Update(updatedLid);
            await _context.SaveChangesAsync();
        }
    }
}
