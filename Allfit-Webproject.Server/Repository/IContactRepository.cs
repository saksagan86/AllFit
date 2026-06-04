using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IContactRepository
    {
        /// <summary>
        /// Sla een contactformulier op en retourneer het opgeslagen object.
        /// </summary>
        Task<ContactFormulier> AddAsync(ContactFormulier contactFormulier);
    }
}