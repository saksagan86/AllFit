using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ICoachingRepository
    {
        /// <summary>
        /// Haal alle actieve doelen op uit de database.
        /// </summary>
        Task<List<Doel>> HaalActieveDoelenOpAsync();
        /// <summary>
        /// Controleer of een doel bestaat en actief is.
        /// </summary>
        Task<bool> DoelBestaatEnIsActiefAsync(int doelId);

        /// <summary>
        /// Haal het gebruikersdoel op voor een specifieke gebruiker.
        /// </summary>
        Task<GebruikerDoel?> HaalGebruikerDoelOpAsync(int gebruikerId);
        /// <summary>
        /// Haal het gebruikersdoel inclusief doelinformatie op voor een gebruiker.
        /// </summary>
        Task<GebruikerDoel?> HaalGebruikerDoelMetDoelOpAsync(int gebruikerId);
        /// <summary>
        /// Voeg een nieuw gebruikersdoel toe aan de database.
        /// </summary>
        Task VoegGebruikerDoelToeAsync(GebruikerDoel gebruikerDoel);

        /// <summary>
        /// Haal het coachingprofiel op voor een gebruiker.
        /// </summary>
        Task<GebruikerCoachingProfiel?> HaalCoachingProfielOpAsync(int gebruikerId);
        /// <summary>
        /// Haal het coachingprofiel inclusief alle gerelateerde data op voor een gebruiker.
        /// </summary>
        Task<GebruikerCoachingProfiel?> HaalCoachingProfielMetAllesOpAsync(int gebruikerId);
        /// <summary>
        /// Voeg een nieuw coachingprofiel toe.
        /// </summary>
        Task VoegCoachingProfielToeAsync(GebruikerCoachingProfiel profiel);

        /// <summary>
        /// Zoek een advies template op basis van doel en gebruikersprofielkenmerken.
        /// </summary>
        Task<AdviesTemplate?> ZoekAdviesTemplateAsync(
            int doelId,
            string activiteitniveau,
            string bmiCategorie
        );

        /// <summary>
        /// Sla voortgang van een training op.
        /// </summary>
        Task VoegTrainingVoortgangToeAsync(TrainingVoortgang voortgang);
        /// <summary>
        /// Haal de voortgangshistorie op voor een gebruikerDoel.
        /// </summary>
        Task<List<TrainingVoortgang>> HaalHistorieOpAsync(int gebruikerDoelId);
        /// <summary>
        /// Haal alle voortgangshistorie op voor een gebruikerDoel.
        /// </summary>
        Task<List<TrainingVoortgang>> HaalAlleHistorieOpAsync(int gebruikerDoelId);
        /// <summary>
        /// Controleer of er al een training in de opgegeven week is geregistreerd.
        /// </summary>
        Task<bool> HeeftTrainingDezeWeekAsync(
            int gebruikerDoelId,
            string trainingsDag,
            DateTime weekStartDatum
        );

        /// <summary>
        /// Bewaar alle openstaande wijzigingen (commit naar database).
        /// </summary>
        Task SaveChangesAsync();
    }
}