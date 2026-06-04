using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IVoedingsadviesRepository
    {
        /// <summary>
        /// Haal alle actieve doelen op.
        /// </summary>
        Task<List<Doel>> HaalActieveDoelenOpAsync();
        /// <summary>
        /// Haal het gebruikersdoel inclusief voedingsschema op voor een gebruiker.
        /// </summary>
        Task<GebruikerDoel?> HaalGebruikerDoelMetSchemaOpAsync(int gebruikerId);
        /// <summary>
        /// Controleer of een doel bestaat en actief is.
        /// </summary>
        Task<bool> DoelBestaatEnIsActiefAsync(int doelId);
        /// <summary>
        /// Haal het gebruikersdoel op voor een gebruiker.
        /// </summary>
        Task<GebruikerDoel?> HaalGebruikerDoelOpAsync(int gebruikerId);
        /// <summary>
        /// Voeg een gebruikersdoel toe.
        /// </summary>
        Task VoegGebruikerDoelToeAsync(GebruikerDoel gebruikerDoel);
        /// <summary>
        /// Bewaar openstaande wijzigingen naar de database.
        /// </summary>
        Task SaveChangesAsync();
    }
}