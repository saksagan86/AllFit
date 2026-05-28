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
        Task VoegTrainingVoortgangToeAsync(TrainingVoortgang voortgang);
        Task<List<TrainingVoortgang>> HaalHistorieOpAsync(int gebruikerDoelId);
        Task SaveChangesAsync();
    }
}