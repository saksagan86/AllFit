using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface IFormService
    {
        Task SaveFormAsync(Dtos.IntakeDTO formData);
        Task SaveProeflesAsync(ProeflesDTO formData);
    }
}
