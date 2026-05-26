using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public class LesService : ILesService
    {

        private readonly ILesRepository _repo;
        private readonly IInschrijfRepository _irepo;

        public LesService(ILesRepository repo, IInschrijfRepository irepo)
        {
            _repo = repo;
            _irepo = irepo;
        }

        public async Task<List<LesDto>> GetLessenByAanbodAsync(int aanbodId, int lidId) { 
            var lessen = await _repo.GetLessenByAanbodIdAsync(aanbodId);
            var ingeschrevenlessen = await _irepo.GetLesIdsVanLidAsync(lidId);
            var result = new List<LesDto>();
            foreach (var les in lessen)
            {
                var aantalIngeschreven = await _irepo.GetAantalIngeschrevenAsync(les.Id);
                result.Add(new LesDto
                {
                    Id = les.Id,
                    Datum = les.Datum,
                    Tijd = les.Tijd,
                    MaxDeelnemers = les.MaxDeelnemers,
                    VrijePlekken = les.MaxDeelnemers - aantalIngeschreven,
                    IsIngeschreven = ingeschrevenlessen.Contains(les.Id)
                });
            }
            return result;
        }

    }
}
