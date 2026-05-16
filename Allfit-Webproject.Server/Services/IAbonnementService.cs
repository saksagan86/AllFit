using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface IAbonnementService
    {
        Task RegisterAbonnementAsync(AbonnementDTO dto);
    }
}
