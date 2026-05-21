using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IContactRepository
    {
        Task<ContactFormulier> AddAsync(ContactFormulier contactFormulier);
    }
}