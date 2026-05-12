using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ILidRepository
    {
        Task AddLidAsync(Lid lid);
        Task<Lid?> GetByEmailAsync(string email);
        Task<Lidmaatschap?> GetLidmaatschapById(int lidmaatschapId);
    }
}
