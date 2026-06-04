using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IAanbodRepository
    {
        /// <summary>
        /// Haal de fitnessaanbiedingen op voor een specifieke sportschool.
        /// </summary>
        Task<List<Fitness>> GetFitnessAsync(int sportschoolId);
        /// <summary>
        /// Haal de groepslessen op voor een specifieke sportschool.
        /// </summary>
        Task<List<Groepsles>> GetGroepslesAsync(int sportschoolId);
        /// <summary>
        /// Haal de kickboks-aanbiedingen op voor een specifieke sportschool.
        /// </summary>
        Task<List<Kickboks>> GetKickboksenAsync(int sportschoolId);
        /// <summary>
        /// Haal extra begeleiding-aanbod op voor een specifieke sportschool.
        /// </summary>
        Task<List<Aanbod>> GetExtraBegeleidingAsync(int sportschoolId);
        /// <summary>
        /// Haal alle aanboditems op voor een specifieke sportschool.
        /// </summary>
        Task<List<Aanbod>> GetAllAanbodAsync(int sportschoolId);
        /// <summary>
        /// Haal alle aanboditems op voor alle sportscholen.
        /// </summary>
        Task<List<Aanbod>> GetAlleAanbodAsync();
    }
}