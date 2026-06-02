using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface ICoachingRepository
    {
        Task<List<Doel>> HaalActieveDoelenOpAsync();
        Task<bool> DoelBestaatEnIsActiefAsync(int doelId);

        Task<GebruikerDoel?> HaalGebruikerDoelOpAsync(int gebruikerId);
        Task<GebruikerDoel?> HaalGebruikerDoelMetDoelOpAsync(int gebruikerId);
        Task VoegGebruikerDoelToeAsync(GebruikerDoel gebruikerDoel);

        Task<GebruikerCoachingProfiel?> HaalCoachingProfielOpAsync(int gebruikerId);
        Task<GebruikerCoachingProfiel?> HaalCoachingProfielMetAllesOpAsync(int gebruikerId);
        Task VoegCoachingProfielToeAsync(GebruikerCoachingProfiel profiel);

        Task<AdviesTemplate?> ZoekAdviesTemplateAsync(
            int doelId,
            string activiteitniveau,
            string bmiCategorie
        );

        Task VoegTrainingVoortgangToeAsync(TrainingVoortgang voortgang);
        Task<List<TrainingVoortgang>> HaalHistorieOpAsync(int gebruikerDoelId);
        Task<List<TrainingVoortgang>> HaalAlleHistorieOpAsync(int gebruikerDoelId);
        Task<bool> HeeftTrainingDezeWeekAsync(
            int gebruikerDoelId,
            string trainingsDag,
            DateTime weekStartDatum
        );

        Task SaveChangesAsync();
    }
}