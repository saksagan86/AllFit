using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface IVoedingsadviesService
    {
        Task<MijnVoedingsschemaResponseDto> GetMijnSchemaAsync(int gebruikerId);
        Task<MijnVoedingsschemaResponseDto?> KiesDoelAsync(int gebruikerId, KiesVoedingsDoelDto dto);
    }
}