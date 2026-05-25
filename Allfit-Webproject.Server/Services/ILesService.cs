using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface ILesService
    {
        Task<List<LesDto>> GetLessenByAanbodAsync(int aanbodId, int lesId);


    }
}
