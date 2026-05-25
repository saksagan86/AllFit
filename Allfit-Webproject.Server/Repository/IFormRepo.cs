using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IFormRepo
    {
        Task SaveFormAsync(Form form);
        Task SaveProeflesAsync(Proefles proefles);
    }
}
