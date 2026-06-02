using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
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

            var toegestaneTypes = new[] { "Kickboks", "Fitness", "Groepsles" };

            if (aanbodType != null && !toegestaneTypes.Contains(aanbodType))
                throw new Exception($"Ongeldig aanbodtype: {aanbodType}");

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

        public async Task<VerhaalDetailDto?> GetVerhaalByIdAsync(int id)
        {
            var verhaal = await _repo.GetVerhaalByIdAsync(id);
            if (verhaal == null) return null;
            return new VerhaalDetailDto
            {
                Id = verhaal.Id,
                Titel = verhaal.Titel,
                Inhoud = verhaal.Inhoud,
                Fotos = verhaal.Fotos,
                GeplaatstOp = verhaal.GeplaatstOp,
                GebruikersNaam = verhaal.Lid.naam,
                AanbodType = verhaal.Aanbod.GetType().Name
            };
        }

        public async Task VerhaalToevoegenAsync(int lidId, VerhaalToevoegenDto dto)
        {
            var fotoUrls = new List<string>();

            foreach (var foto in dto.Fotos ?? new List<IFormFile>())
            {
                var bestandsnaam = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                var pad = Path.Combine(Directory.GetCurrentDirectory(), "..", "allfit-webproject.client", "public", "images", bestandsnaam);

                using (var stream = new FileStream(pad, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                fotoUrls.Add($"/images/{bestandsnaam}");
            }

            var verhaal = new Verhaal
            {
                Titel = dto.Titel,
                Inhoud = dto.Inhoud,
                Image = dto.Image,
                AanbodId = dto.AanbodId,
                LidId = lidId,
                GeplaatstOp = DateTime.UtcNow,
                Fotos = fotoUrls
            };

            await _repo.VerhaalToevoegenAsync(verhaal);
        }

    }
}
