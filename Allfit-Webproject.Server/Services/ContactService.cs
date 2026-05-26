using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContactFormulierResponseDto> MaakContactBerichtAsync(ContactFormulierCreateDto dto)
        {
            var contactFormulier = new ContactFormulier
            {
                naam = dto.Naam.Trim(),
                email = dto.Email.Trim().ToLower(),
                titel = dto.Titel.Trim(),
                beschrijving = dto.Beschrijving.Trim(),
                aangemaaktOp = DateTime.UtcNow,
                status = "Nieuw"
            };

            var opgeslagenBericht = await _repository.AddAsync(contactFormulier);

            return new ContactFormulierResponseDto
            {
                Id = opgeslagenBericht.id,
                Bericht = "Bedankt voor je bericht. AllFit neemt zo snel mogelijk contact met je op.",
                AangemaaktOp = opgeslagenBericht.aangemaaktOp
            };
        }
    }
}