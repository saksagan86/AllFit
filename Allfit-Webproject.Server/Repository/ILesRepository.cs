using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ILesRepository
    {

        Task<List<Les>> GetLessenByAanbodIdAsync(int aanbodId);
        Task<Les> GetLesByIdAsync(int lesId);

    }
}
