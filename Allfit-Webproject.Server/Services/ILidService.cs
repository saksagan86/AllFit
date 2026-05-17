using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface ILidService
    {
        Task RegisterLidAsync(RegisterLidDTO dto);
    }
}
