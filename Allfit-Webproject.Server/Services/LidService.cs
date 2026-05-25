namespace Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;
using System.Threading.Tasks;

public class LidService : ILidService
{
    private readonly ILidRepository _repo;

    public LidService(ILidRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> RegisterLidAsync(RegisterLidDTO dto)
    {

        if (dto.Wachtwoord != dto.BevestigWachtwoord)
            throw new Exception("Wachtwoorden komen niet overeen");

        if (!dto.AccepteerVoorwaarden)
            throw new Exception("Je moet akkoord gaan met de voorwaarden");

        var lidmaatschap = await _repo.GetLidmaatschapById(dto.LidmaatschapId);

        if (lidmaatschap == null)
            throw new Exception("Ongeldig lidmaatschap");

        var existing = await _repo.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Email bestaat al");

        var gehashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(dto.Wachtwoord);

        var lid = new Lid
        {
            naam = dto.Naam,
            email = dto.Email,
            wachtwoord = gehashedWachtwoord,
            telefoonnummer = dto.Telefoonnummer,
            geboortedatum = dto.Geboortedatum,
            adres = dto.Adres,
            huisnummer = dto.Huisnummer,
            woonplaats = dto.Woonplaats,
            postcode = dto.Postcode,
            lidmaatschapId = dto.LidmaatschapId,
            isActief = false
        };

        await _repo.AddLidAsync(lid);
        return lid.id;
    }

    public async Task<GegevensLidDto> GetLidByIdAsync(int lidId) {
        Lid lid = await _repo.GetLidByIdAsync(lidId);

        if (lid == null) { 
            return null;
        }
        
        return new GegevensLidDto
        {
            Naam = lid.naam,
            Email = lid.email,
            Telefoonnummer = lid.telefoonnummer,
            Adres = lid.adres,
            Huisnummer = lid.huisnummer,
            Postcode = lid.postcode,
            Stad = lid.woonplaats
        };
    }

    public async Task UpdateLidAsync(int lidId, UpdateLidDto dto)
    {
        var lid = await _repo.GetLidByIdAsync(lidId);
        if (lid == null) throw new Exception("Lid niet gevonden");

        lid.naam = dto.Naam;
        lid.email = dto.Email;
        lid.telefoonnummer = dto.Telefoonnummer;
        lid.adres = dto.Adres;
        lid.huisnummer = dto.Huisnummer;
        lid.postcode = dto.Postcode;
        lid.woonplaats = dto.Stad;

        await _repo.UpdateLidAsync(lid);
    }

}