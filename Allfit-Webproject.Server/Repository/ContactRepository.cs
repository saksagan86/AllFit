using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactFormulier> AddAsync(ContactFormulier contactFormulier)
        {
            _context.ContactFormulieren.Add(contactFormulier);
            await _context.SaveChangesAsync();
            return contactFormulier;
        }
    }
}