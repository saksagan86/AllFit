using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IVoedingsadviesRepository
    {
        Task<List<Doel>> HaalActieveDoelenOpAsync();
        Task<GebruikerDoel?> HaalGebruikerDoelMetSchemaOpAsync(int gebruikerId);
        Task<bool> DoelBestaatEnIsActiefAsync(int doelId);
        Task<GebruikerDoel?> HaalGebruikerDoelOpAsync(int gebruikerId);
        Task VoegGebruikerDoelToeAsync(GebruikerDoel gebruikerDoel);
        Task SaveChangesAsync();
    }
}