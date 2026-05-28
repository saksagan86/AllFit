using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public class VerhaalService : IVerhaalService
    {

        private readonly IVerhaalRepository _repo;
    
            public VerhaalService(IVerhaalRepository repo)
            {
                _repo = repo;
            }
    
            public async Task<List<VerhaalDto>> GetVerhalenAsync(string? aanbodType)
            {
                var verhalen = await _repo.GetVerhalenAsync(aanbodType);
                return verhalen.Select(v => new VerhaalDto
                {
                    Id = v.Id,
                    Titel = v.Titel,
                    Inhoud = v.Inhoud,
                    Image = v.Image,
                    GeplaatstOp = v.GeplaatstOp,
                    GebruikersNaam = v.Lid.naam,
                    AanbodType = v.Aanbod.GetType().Name
                }).ToList();
        }
    }
}
