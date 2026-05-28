using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Services
{
    public interface IContactService
    {
        Task<ContactFormulierResponseDto> MaakContactBerichtAsync(ContactFormulierCreateDto dto);
    }
}