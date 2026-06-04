using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ILesRepository
    {

        /// <summary>
        /// Haal lessen op voor een bepaald aanbod (aanbodId).
        /// </summary>
        Task<List<Les>> GetLessenByAanbodIdAsync(int aanbodId);
        /// <summary>
        /// Haal een specifieke les op via lesId.
        /// </summary>
        Task<Les> GetLesByIdAsync(int lesId);

    }
}
