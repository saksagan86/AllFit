using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepo _repo;

        public FormService(IFormRepo repo)
        {
            _repo = repo;
        }
        public async Task SaveFormAsync(IntakeDTO formData)
        {
            var form = new Form
            {
                Naam = formData.Naam,
                Email = formData.Email,
                Telefoon = formData.Telefoon,
                Bericht = formData.Bericht
            };
            await _repo.SaveFormAsync(form);
        }

        public async Task SaveProeflesAsync(ProeflesDTO formData)
        {
            var proefles = new Proefles
            {
                Name = formData.Name,
                Email = formData.Email,
                Telefoon = formData.Telefoon,
                SportschoolID = formData.SportschoolID,
                LesID = formData.LesID
            };
            await _repo.SaveProeflesAsync(proefles);
        }
    }
}
