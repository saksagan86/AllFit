using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Services
{
    public interface IVerhaalService
    {

        Task<List<VerhaalDto>> GetVerhalenAsync(string? aanbodType);
        Task<VerhaalDetailDto?> GetVerhaalByIdAsync(int id);
        Task VerhaalToevoegenAsync(int lidId, VerhaalToevoegenDto dto);
    }
}
