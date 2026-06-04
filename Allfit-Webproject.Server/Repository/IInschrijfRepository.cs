using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IInschrijfRepository
    {

        /// <summary>
        /// Schrijf een gebruiker in voor een les.
        /// </summary>
        Task InschrijvenAsync(Inschrijving inschrijving);
        /// <summary>
        /// Controleer of een lid al is ingeschreven voor een specifieke les.
        /// </summary>
        Task<bool> IsAlIngeschrevenAsync(int lidId, int lesId);
        /// <summary>
        /// Haal het aantal ingeschrevenen voor een les op.
        /// </summary>
        Task<int> GetAantalIngeschrevenAsync(int lesId);
        /// <summary>
        /// Haal de lijst met les-IDs waarop een lid is ingeschreven.
        /// </summary>
        Task<List<int>> GetLesIdsVanLidAsync(int lidId);
        /// <summary>
        /// Haal alle inschrijvingen van een bepaald lid op.
        /// </summary>
        Task<List<Inschrijving>> GetInschrijvingenVanLidAsync(int lidId);
        /// <summary>
        /// Verwijder een inschrijving op basis van ID.
        /// </summary>
        Task <bool> DeleteInschrijvingByIdAsync(int inschijvingId);
        /// <summary>
        /// Haal een inschrijving op via zijn ID.
        /// </summary>
        Task<Inschrijving?> GetInschrijvingByIdAsync(int inschrijvingId);
        /// <summary>
        /// Bewaar openstaande wijzigingen naar de database.
        /// </summary>
        Task SaveChangesAsync();
    }
}
