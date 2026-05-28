using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public class VoedingsadviesService : IVoedingsadviesService
    {
        private readonly IVoedingsadviesRepository _repository;

        public VoedingsadviesService(IVoedingsadviesRepository repository)
        {
            _repository = repository;
        }

        public async Task<MijnVoedingsschemaResponseDto> GetMijnSchemaAsync(int gebruikerId)
        {
            var beschikbareDoelen = await HaalBeschikbareDoelenDtoOpAsync();

            var gebruikerDoel = await _repository.HaalGebruikerDoelMetSchemaOpAsync(gebruikerId);

            if (gebruikerDoel == null)
            {
                return new MijnVoedingsschemaResponseDto
                {
                    HeeftDoel = false,
                    BeschikbareDoelen = beschikbareDoelen
                };
            }

            var schema = gebruikerDoel.doel?.voedingsschemas.FirstOrDefault();

            if (schema == null)
            {
                return new MijnVoedingsschemaResponseDto
                {
                    HeeftDoel = false,
                    BeschikbareDoelen = beschikbareDoelen,
                    Message = "Er is nog geen voedingsschema gekoppeld aan jouw gekozen doel."
                };
            }

            return new MijnVoedingsschemaResponseDto
            {
                HeeftDoel = true,
                BeschikbareDoelen = beschikbareDoelen,
                Doel = new BeschikbaarVoedingsDoelDto
                {
                    Id = gebruikerDoel.doel!.id,
                    Naam = gebruikerDoel.doel.naam,
                    Beschrijving = gebruikerDoel.doel.beschrijving
                },
                Schema = new VoedingsschemaDto
                {
                    Id = schema.id,
                    Titel = schema.titel,
                    Beschrijving = schema.beschrijving,
                    Regels = schema.regels
                        .OrderBy(r => r.volgorde)
                        .Select(r => new VoedingsschemaRegelDto
                        {
                            Id = r.id,
                            MaaltijdMoment = r.maaltijdMoment,
                            Advies = r.advies,
                            Volgorde = r.volgorde
                        })
                        .ToList()
                }
            };
        }

        public async Task<MijnVoedingsschemaResponseDto?> KiesDoelAsync(int gebruikerId, KiesVoedingsDoelDto dto)
        {
            var doelBestaat = await _repository.DoelBestaatEnIsActiefAsync(dto.DoelId);

            if (!doelBestaat)
            {
                return null;
            }

            var gebruikerDoel = await _repository.HaalGebruikerDoelOpAsync(gebruikerId);

            if (gebruikerDoel == null)
            {
                gebruikerDoel = new GebruikerDoel
                {
                    gebruikerId = gebruikerId,
                    doelId = dto.DoelId,
                    aangemaaktOp = DateTime.UtcNow,
                    gewijzigdOp = DateTime.UtcNow
                };

                await _repository.VoegGebruikerDoelToeAsync(gebruikerDoel);
            }
            else
            {
                gebruikerDoel.doelId = dto.DoelId;
                gebruikerDoel.gewijzigdOp = DateTime.UtcNow;
            }

            await _repository.SaveChangesAsync();

            return await GetMijnSchemaAsync(gebruikerId);
        }

        private async Task<List<BeschikbaarVoedingsDoelDto>> HaalBeschikbareDoelenDtoOpAsync()
        {
            var doelen = await _repository.HaalActieveDoelenOpAsync();

            return doelen
                .Select(d => new BeschikbaarVoedingsDoelDto
                {
                    Id = d.id,
                    Naam = d.naam,
                    Beschrijving = d.beschrijving
                })
                .ToList();
        }
    }
}