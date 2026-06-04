using Allfit_Webproject.Server.Models;
namespace Allfit_Webproject.Server.Repository
{
    public interface ISportschoolRepository
    {
        /// <summary>
        /// Haal alle sportscholen inclusief gerelateerde entiteiten (openingstijden, faciliteiten).
        /// </summary>
        Task<List<Sportschool>> GetAllSportscholenAsync();
    }
}
