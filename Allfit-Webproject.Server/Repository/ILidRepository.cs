using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ILidRepository
    {
        /// <summary>
        /// Voeg een nieuw lid toe en retourneer het nieuw aangemaakte id.
        /// </summary>
        Task<int> AddLidAsync(Lid lid);
        /// <summary>
        /// Haal een lid op via e-mailadres.
        /// </summary>
        Task<Lid?> GetByEmailAsync(string email);
        /// <summary>
        /// Haal een lidmaatschap op via id.
        /// </summary>
        Task<Lidmaatschap?> GetLidmaatschapById(int lidmaatschapId);
        /// <summary>
        /// Haal een lid op via zijn id.
        /// </summary>
        Task<Lid?> GetLidByIdAsync(int lidId);
        /// <summary>
        /// Werk de gegevens van een bestaand lid bij.
        /// </summary>
        Task UpdateLidAsync(Lid updatedLid);
    }
}
