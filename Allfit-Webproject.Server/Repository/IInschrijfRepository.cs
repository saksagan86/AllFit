using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IInschrijfRepository
    {

        Task InschrijvenAsync(Inschrijving inschrijving);
        Task<bool> IsAlIngeschrevenAsync(int lidId, int lesId);
        Task<int> GetAantalIngeschrevenAsync(int lesId);
        Task<List<int>> GetLesIdsVanLidAsync(int lidId);
    }
}
