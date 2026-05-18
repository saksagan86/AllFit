using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Repository;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Services
{
    public class AbonnementService : IAbonnementService
    {
        private readonly IAbonnementRepository _repo;

        public AbonnementService(IAbonnementRepository repo)
        {
            _repo = repo;
        }

        public async Task RegisterAbonnementAsync(AbonnementDTO dto)
        {
            var Abonnement = new Abonnement
            {
                LidID = dto.LidID,
                LidmaatschapID = dto.LidmaatschapID,
                MollieID = dto.MollieID,
                DateStart = dto.DateStart,
                DateEnd = dto.DateEnd
            };
            await _repo.AddAbonnementAsync(Abonnement);
        }
        public async Task<Lid?> GetLid(String mollieID)
        {
            return await _repo.GetByMollieIDAsync(mollieID);
        }
    }
}
