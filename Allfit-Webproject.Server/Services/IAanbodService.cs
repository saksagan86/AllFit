using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface IAanbodService
    {

        Task<List<FitnessDTO>> GetFitnessAsync(int sportschoolId);
        Task<List<GroepslesDTO>> GetGroepslesAsync(int sportschoolId);
        Task<List<KickboksDTO>> GetKickboksenAsync(int sportschoolId);
        Task<List<ExtraBegeleidingDTO>> GetExtraBegeleidingAsync(int sportschoolId);
        Task<List<AanbodDTO>> GetAllAsync(int sportschoolId);
    }
}
