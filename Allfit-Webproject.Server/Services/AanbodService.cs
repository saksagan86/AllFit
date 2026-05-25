using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;
using Allfit_Webproject.Server.Services;

public class AanbodService : IAanbodService
{
    private readonly IAanbodRepository _repo;

    public AanbodService(IAanbodRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<FitnessDTO>> GetFitnessAsync(int id)
    {
        var data = await _repo.GetFitnessAsync(id);

        return data.Select(x => new FitnessDTO
        {
            Id = x.id,
            Naam = x.naam,
            Image = x.image
        }).ToList();
    }

    public async Task<List<GroepslesDTO>> GetGroepslesAsync(int id)
    {
        var data = await _repo.GetGroepslesAsync(id);

        return data.Select(x => new GroepslesDTO
        {
            Id = x.id,
            Naam = x.naam,
            Image = x.image,
            Niveau = x.niveau,
            Duur = x.duur,
            ExtraBegeleiding = x.ExtraBegeleiding,
            BeschrijvingBegeleiding = x.BeschrijvingBegeleiding,
            TrainerId = x.TrainerId,
            TrainerNaam = x.trainer != null ? x.trainer.naam : null
        }).ToList();
    }


    public async Task<List<KickboksDTO>> GetKickboksenAsync(int id)
    {
        var data = await _repo.GetKickboksenAsync(id);

        return data.Select(x => new KickboksDTO
        {
            Id = x.id,
            Naam = x.naam,
            Image = x.image,
            Doelgroep = x.doelgroep,
            ExtraBegeleiding = x.ExtraBegeleiding,
            BeschrijvingBegeleiding = x.BeschrijvingBegeleiding,
            TrainerId = x.TrainerId,
            TrainerNaam = x.trainer != null ? x.trainer.naam : null
        }).ToList();
    }

    public async Task<List<ExtraBegeleidingDTO>> GetExtraBegeleidingAsync(int sportschoolId)
    {
        var data = await _repo.GetExtraBegeleidingAsync(sportschoolId);

        return data.Select(x => new ExtraBegeleidingDTO
        {
            Id = x.id,
            Naam = x.naam,
            Image = x.image,
            BeschrijvingBegeleiding = x.BeschrijvingBegeleiding,
            TrainerNaam = x.trainer != null ? x.trainer.naam : null,
            SportType = x switch
            {
                Fitness => "fitness",
                Groepsles => "groepsles",
                Kickboks => "kickboks",
                _ => "onbekend"
            },
            Niveau = x is Groepsles g ? g.niveau : null,
            Duur = x is Groepsles gr ? gr.duur : null,
            Doelgroep = x is Kickboks k ? k.doelgroep : null
        }).ToList();
    }

    public async Task<List<AanbodDTO>> GetAllAsync(int sportschoolId)
    {
        var data = await _repo.GetAllAanbodAsync(sportschoolId);

        return data.Select(x => new AanbodDTO
        {
            Id = x.id,
            Naam = x.naam,
            SportType = x switch
            {
                Fitness => "fitness",
                Groepsles => "groepsles",
                Kickboks => "kickboks",
                _ => "onbekend"
            }
        }).ToList();
    }
}