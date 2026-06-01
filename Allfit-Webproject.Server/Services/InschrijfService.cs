using Allfit_Webproject.Server.Repository;
using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
namespace Allfit_Webproject.Server.Services
{
    public class InschrijfService : IInschrijvingService
    {
        private readonly IInschrijfRepository _repo;
        private readonly ILesRepository _lesRepo;

        public InschrijfService(IInschrijfRepository _repo, ILesRepository _lesRepo) {
            this._repo = _repo;
            this._lesRepo = _lesRepo;
        }

        public async Task AddInschrijvingenAsync(InschrijfDto dto, int lidId) { 
            foreach(int lesId in dto.LesIds){

                var les = await _lesRepo.GetLesByIdAsync(lesId);
                if (les == null)
                    throw new Exception($"Les met id {lesId} bestaat niet");

                var aantalIngeschreven = await _repo.GetAantalIngeschrevenAsync(lesId);
                if (aantalIngeschreven >= les.MaxDeelnemers)
                    throw new Exception($"Les {lesId} is vol");

                var alIngeschreven = await _repo.IsAlIngeschrevenAsync(lidId, lesId);
                if (alIngeschreven)
                    throw new Exception($"Je bent al ingeschreven voor deze les");

                var inschrijving = new Inschrijving
                {
                    LidId = lidId,
                    LesId = lesId,
                    ExtraBegeleiding = dto.ExtraBegeleiding
                };
                await _repo.InschrijvenAsync(inschrijving);
            }
        }

        public async Task<List<InschrijvingDto>> GetInschrijvingenVanLidAsync(int lidId) {
            var inschrijvingen = await _repo.GetInschrijvingenVanLidAsync(lidId);
            return inschrijvingen.Select(i => new InschrijvingDto
            {
                Id = i.Id,
                Datum = i.Les.Datum,
                Tijd = i.Les.Tijd,
                AanbodNaam = i.Les.Aanbod.naam,
                ExtraBegeleiding = i.ExtraBegeleiding
            }).ToList();
        }

        public async Task DeleteInschrijvingByIdAsync(int inschrijvingId, int lidId)
        {
            var inschrijving = await _repo.GetInschrijvingByIdAsync(inschrijvingId);
            if (inschrijving == null)
                throw new Exception("Inschrijving niet gevonden");
            if (inschrijving.LidId != lidId)
                throw new Exception("Je hebt geen toegang tot deze inschrijving");

            await _repo.DeleteInschrijvingByIdAsync(inschrijvingId);
        }
    }
}
