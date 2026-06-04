using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IVerhaalRepository
    {
        /// <summary>
        /// Haal verhalen op, optioneel gefilterd op aanbodtype.
        /// </summary>
        Task<List<Verhaal>> GetVerhalenAsync(string? aanbodType);
        /// <summary>
        /// Haal een specifiek verhaal op via id.
        /// </summary>
        Task<Verhaal?> GetVerhaalByIdAsync(int id);
        /// <summary>
        /// Voeg een nieuw verhaal toe aan de database.
        /// </summary>
        Task VerhaalToevoegenAsync(Verhaal verhaal);
    }
}
