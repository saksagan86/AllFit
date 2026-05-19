using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface ILidService
    {
        Task<int> RegisterLidAsync(RegisterLidDTO dto);
    }
}
