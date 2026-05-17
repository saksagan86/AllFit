using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface ISportschoolService
    {

        Task <List<ToonSportschoolDTO>> ToonAlleSportscholenOpAsync();

    }
}
