using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Repository;
using Allfit_Webproject.Server.Models;  
using Microsoft.Identity.Client;

namespace Allfit_Webproject.Server.Services
{
    public class SportschoolService : ISportschoolService
    {

        private readonly ISportschoolRepository _repo;

        public SportschoolService(ISportschoolRepository repo)
        {

            _repo = repo;
        }

        public async Task<List<ToonSportschoolDTO>> ToonAlleSportscholenOpAsync()
        {
            var sportscholen = await _repo.GetAllSportscholenAsync();

            return sportscholen.Select(s => new ToonSportschoolDTO {
                Id = s.id, 
                Naam = s.naam,
                Stad = s.stad,
                Adres = s.adres,
                Coordinaten = [s.lon, s.lat],
                Openingstijden = s.openingstijdenSportschool.Select(o => new ToonOpeningstijdInfoDTO
                {
                    Dag = o.dag,
                    TijdOpen = o.tijdOpen,
                    TijdSluit = o.tijdSluit
                }).ToList(),
                Faciliteiten = s.alleFaciliteiten.Select(f => new ToonFaciliteitInfoDTO
                {
                    Naam = f.naam,
                    Beschrijving = f.beschrijving
                }).ToList()
            }).ToList();

        }
    }
}
