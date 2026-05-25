using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public class FormRepo : IFormRepo
    {
        private readonly ApplicationDbContext _context;

        public FormRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveFormAsync(Form form)
        {
            _context.Form.Add(form);
            await _context.SaveChangesAsync();
        }

        public async Task SaveProeflesAsync(Proefles proefles)
        {
            _context.Proefles.Add(proefles);
            await _context.SaveChangesAsync();
        }
    }
}
