using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class VoedingsadviesRepository : IVoedingsadviesRepository
    {
        private readonly ApplicationDbContext _context;

        public VoedingsadviesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doel>> HaalActieveDoelenOpAsync()
        {
            return await _context.Doelen
                .Where(d => d.actief)
                .OrderBy(d => d.id)
                .ToListAsync();
        }

        public async Task<GebruikerDoel?> HaalGebruikerDoelMetSchemaOpAsync(int gebruikerId)
        {
            return await _context.GebruikerDoelen
                .Include(gd => gd.doel)
                    .ThenInclude(d => d!.voedingsschemas)
                        .ThenInclude(v => v.regels)
                .FirstOrDefaultAsync(gd => gd.gebruikerId == gebruikerId);
        }

        public async Task<bool> DoelBestaatEnIsActiefAsync(int doelId)
        {
            return await _context.Doelen
                .AnyAsync(d => d.id == doelId && d.actief);
        }

        public async Task<GebruikerDoel?> HaalGebruikerDoelOpAsync(int gebruikerId)
        {
            return await _context.GebruikerDoelen
                .FirstOrDefaultAsync(gd => gd.gebruikerId == gebruikerId);
        }

        public async Task VoegGebruikerDoelToeAsync(GebruikerDoel gebruikerDoel)
        {
            await _context.GebruikerDoelen.AddAsync(gebruikerDoel);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}