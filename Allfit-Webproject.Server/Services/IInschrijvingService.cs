using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public interface IInschrijvingService
    {

        Task AddInschrijvingenAsync(InschrijfDto dto, int lidId);

    }
}
