using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ILidRepository
    {
        Task<int> AddLidAsync(Lid lid);
        Task<Lid?> GetByEmailAsync(string email);
        Task<Lidmaatschap?> GetLidmaatschapById(int lidmaatschapId);
        Task<Lid?> GetLidByIdAsync(int lidId);
        Task UpdateLidAsync(Lid updatedLid);
    }
}
