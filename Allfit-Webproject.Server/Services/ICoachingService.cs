using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface ICoachingService
    {
        Task<CoachingDashboardDto> GetDashboardAsync(int gebruikerId);
        Task<CoachingDashboardDto?> KiesDoelAsync(int gebruikerId, KiesCoachingDoelDto dto);
        Task<CoachingDashboardDto?> RondTrainingAfAsync(int gebruikerId, TrainingAfrondenDto dto);

        Task<CoachingDashboardDto?> MaakOfUpdateProfielAsync(
            int gebruikerId,
            CoachingProfielAanvraagDto dto
        );

        Task<CoachingDashboardDto?> SlaWeekVoortgangOpAsync(
            int gebruikerId,
            WeekVoortgangOpslaanDto dto
        );
    }
}