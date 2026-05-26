using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IAanbodRepository
    {
        Task<List<Fitness>> GetFitnessAsync(int sportschoolId);
        Task<List<Groepsles>> GetGroepslesAsync(int sportschoolId);
        Task<List<Kickboks>> GetKickboksenAsync(int sportschoolId);
        Task<List<Aanbod>> GetExtraBegeleidingAsync(int sportschoolId);
        Task<List<Aanbod>> GetAllAanbodAsync(int sportschoolId);
    }
}