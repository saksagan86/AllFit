using Allfit_Webproject.Server.Models;
namespace Allfit_Webproject.Server.Repository
{
    public interface ISportschoolRepository
    {
        Task<List<Sportschool>> GetAllSportscholenAsync();
    }
}
