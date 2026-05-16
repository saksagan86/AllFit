using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class AbonnementRepository : IAbonnementRepository
    {
        private readonly ApplicationDbContext _context;

        public AbonnementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAbonnementAsync(Abonnement abo)
        {
            _context.Abonnement.Add(abo);
            await _context.SaveChangesAsync();
        }

        public async Task<Lid?> GetLid(String mollie)
        {
            var lid = _context.Abonnement.FirstOrDefault(x => x.MollieID == mollie);
            return await _context.Lid.FirstOrDefaultAsync(x => x.id == lid.LidID);
        }
    }
}
