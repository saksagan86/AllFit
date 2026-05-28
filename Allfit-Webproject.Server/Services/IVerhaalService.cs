using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface IVerhaalService
    {

        Task<List<VerhaalDto>> GetVerhalenAsync(string? aanbodType);

    }
}
