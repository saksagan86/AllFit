using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Repository
{
    public class CoachingRepository : ICoachingRepository
    {
        private readonly ApplicationDbContext _context;

        public CoachingRepository(ApplicationDbContext context)
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

        public async Task<GebruikerDoel?> HaalGebruikerDoelMetDoelOpAsync(int gebruikerId)
        {
            return await _context.GebruikerDoelen
                .Include(gd => gd.doel)
                .FirstOrDefaultAsync(gd => gd.gebruikerId == gebruikerId);
        }

        public async Task VoegGebruikerDoelToeAsync(GebruikerDoel gebruikerDoel)
        {
            await _context.GebruikerDoelen.AddAsync(gebruikerDoel);
        }

        public async Task<GebruikerCoachingProfiel?> HaalCoachingProfielOpAsync(int gebruikerId)
        {
            return await _context.GebruikerCoachingProfielen
                .FirstOrDefaultAsync(cp => cp.gebruikerId == gebruikerId);
        }

        public async Task<GebruikerCoachingProfiel?> HaalCoachingProfielMetAllesOpAsync(int gebruikerId)
        {
            return await _context.GebruikerCoachingProfielen
                .Include(cp => cp.doel)
                .Include(cp => cp.adviesTemplate)
                .FirstOrDefaultAsync(cp => cp.gebruikerId == gebruikerId);
        }

        public async Task VoegCoachingProfielToeAsync(GebruikerCoachingProfiel profiel)
        {
            await _context.GebruikerCoachingProfielen.AddAsync(profiel);
        }

        public async Task<AdviesTemplate?> ZoekAdviesTemplateAsync(
            int doelId,
            string activiteitniveau,
            string bmiCategorie
        )
        {
            var query = _context.AdviesTemplates
                .Where(a => a.doelId == doelId && a.actief);

            var exact = await query.FirstOrDefaultAsync(a =>
                a.activiteitniveau == activiteitniveau &&
                a.bmiCategorie == bmiCategorie
            );

            if (exact != null)
            {
                return exact;
            }

            var algemeenVoorNiveau = await query.FirstOrDefaultAsync(a =>
                a.activiteitniveau == activiteitniveau &&
                a.bmiCategorie == "Algemeen"
            );

            if (algemeenVoorNiveau != null)
            {
                return algemeenVoorNiveau;
            }

            var algemeenVoorDoel = await query.FirstOrDefaultAsync(a =>
                a.activiteitniveau == "Algemeen" &&
                a.bmiCategorie == "Algemeen"
            );

            if (algemeenVoorDoel != null)
            {
                return algemeenVoorDoel;
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task VoegTrainingVoortgangToeAsync(TrainingVoortgang voortgang)
        {
            await _context.TrainingVoortgangen.AddAsync(voortgang);
        }

        public async Task<List<TrainingVoortgang>> HaalHistorieOpAsync(int gebruikerDoelId)
        {
            return await _context.TrainingVoortgangen
                .Where(v => v.gebruikerDoelId == gebruikerDoelId)
                .OrderByDescending(v => v.afgerondOp)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<TrainingVoortgang>> HaalAlleHistorieOpAsync(int gebruikerDoelId)
        {
            return await _context.TrainingVoortgangen
                .Where(v => v.gebruikerDoelId == gebruikerDoelId)
                .OrderByDescending(v => v.afgerondOp)
                .ToListAsync();
        }

        public async Task<bool> HeeftTrainingDezeWeekAsync(
            int gebruikerDoelId,
            string trainingsDag,
            DateTime weekStartDatum
        )
        {
            var weekEindDatum = weekStartDatum.Date.AddDays(7);

            return await _context.TrainingVoortgangen.AnyAsync(v =>
                v.gebruikerDoelId == gebruikerDoelId &&
                v.trainingsDag == trainingsDag &&
                v.afgerondOp.Date >= weekStartDatum.Date &&
                v.afgerondOp.Date < weekEindDatum
            );
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}