using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IFormRepo
    {
        /// <summary>
        /// Sla een algemeen formulier op in de database.
        /// </summary>
        Task SaveFormAsync(Form form);
        /// <summary>
        /// Sla een proefles-aanmelding op in de database.
        /// </summary>
        Task SaveProeflesAsync(Proefles proefles);
    }
}
