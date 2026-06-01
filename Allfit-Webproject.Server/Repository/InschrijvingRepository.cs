using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class InschrijvingRepository : IInschrijfRepository
    {
        private readonly ApplicationDbContext _context;

        public InschrijvingRepository(ApplicationDbContext _context) {
            this._context = _context;
        }


        public async Task InschrijvenAsync(Inschrijving inschrijving) {
            _context.Add(inschrijving);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAlIngeschrevenAsync(int lidId, int lesId) {
            return await _context.Inschrijvingen.AnyAsync(i => i.LesId == lesId && i.LidId == lidId);
        }

        public async Task<int> GetAantalIngeschrevenAsync(int lesId)
        {
            return await _context.Inschrijvingen.Where(i => i.LesId == lesId).CountAsync();
        }

        public async Task<List<int>> GetLesIdsVanLidAsync(int lidId) {
            return await _context.Inschrijvingen.Where(i => i.LidId == lidId).Select(i => i.LesId).ToListAsync();
        }

        public async Task<List<Inschrijving>> GetInschrijvingenVanLidAsync(int lidId) {
            return await _context.Inschrijvingen.Include(i => i.Les).ThenInclude(l => l.Aanbod).Where(i => i.LidId == lidId).ToListAsync();
        }

        public async Task<bool> DeleteInschrijvingByIdAsync(int inschrijvingId)
        {
            var inschrijving = await _context.Inschrijvingen
                .FirstOrDefaultAsync(i => i.Id == inschrijvingId);

            if (inschrijving == null) return false;

            _context.Inschrijvingen.Remove(inschrijving);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Inschrijving?> GetInschrijvingByIdAsync(int inschrijvingId)
        {
            return await _context.Inschrijvingen
                .FirstOrDefaultAsync(i => i.Id == inschrijvingId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
