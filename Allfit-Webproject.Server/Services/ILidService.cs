using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface ILidService
    {
        Task<int> RegisterLidAsync(RegisterLidDTO dto);
        Task<GegevensLidDto> GetLidByIdAsync(int lidId);
        Task UpdateLidAsync(int lidId, UpdateLidDto dto);
    }
}
