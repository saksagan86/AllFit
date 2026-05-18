using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Services
{
    public interface IAbonnementService
    {
        Task RegisterAbonnementAsync(AbonnementDTO dto);
        Task<Lid?> GetLid(String mollieID);
        Task UpdateLidAsync(Lid lid);
    }
}
