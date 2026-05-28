using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IVerhaalRepository
    {
        Task<List<Verhaal>> GetVerhalenAsync(string? aanbodType);

    }
}
