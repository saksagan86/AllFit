namespace Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;

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
            isActief = true
        };

        await _repo.AddLidAsync(lid);
        return lid.id;
    }
}