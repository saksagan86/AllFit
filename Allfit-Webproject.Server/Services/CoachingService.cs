using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;

namespace Allfit_Webproject.Server.Services
{
    public class CoachingService : ICoachingService
    {
        private readonly ICoachingRepository _repository;

        public CoachingService(ICoachingRepository repository)
        {
            _repository = repository;
        }

        public async Task<CoachingDashboardDto> GetDashboardAsync(int gebruikerId)
        {
            var doelen = await _repository.HaalActieveDoelenOpAsync();
            var beschikbareDoelen = doelen.Select(d => new CoachingDoelDto
            {
                Id = d.id,
                Naam = d.naam,
                Beschrijving = d.beschrijving
            }).ToList();

            var gebruikerDoel = await _repository.HaalGebruikerDoelMetDoelOpAsync(gebruikerId);

            if (gebruikerDoel == null || gebruikerDoel.doel == null)
            {
                return new CoachingDashboardDto
                {
                    HeeftDoel = false,
                    BeschikbareDoelen = beschikbareDoelen
                };
            }

            var historie = await _repository.HaalHistorieOpAsync(gebruikerDoel.id);
            var trainingsschema = MaakTrainingsschema(gebruikerDoel.doel.naam);
            var weekDoel = trainingsschema.Count;

            var startVanWeek = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1);

            var afgerondDezeWeek = historie
                .Count(h => h.afgerondOp.Date >= startVanWeek);

            var percentage = weekDoel == 0
                ? 0
                : Math.Min(100, (int)Math.Round((double)afgerondDezeWeek / weekDoel * 100));

            return new CoachingDashboardDto
            {
                HeeftDoel = true,
                BeschikbareDoelen = beschikbareDoelen,
                Doel = new CoachingDoelDto
                {
                    Id = gebruikerDoel.doel.id,
                    Naam = gebruikerDoel.doel.naam,
                    Beschrijving = gebruikerDoel.doel.beschrijving
                },
                Trainingsschema = trainingsschema,
                Progress = new ProgressDto
                {
                    AfgerondDezeWeek = afgerondDezeWeek,
                    WeekDoel = weekDoel,
                    Percentage = percentage
                },
                Historie = historie.Select(h => new TrainingHistorieDto
                {
                    Id = h.id,
                    TrainingsDag = h.trainingsDag,
                    AfgerondOp = h.afgerondOp,
                    Notitie = h.notitie
                }).ToList()
            };
        }

        public async Task<CoachingDashboardDto?> KiesDoelAsync(int gebruikerId, KiesCoachingDoelDto dto)
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

            return await GetDashboardAsync(gebruikerId);
        }

        public async Task<CoachingDashboardDto?> RondTrainingAfAsync(int gebruikerId, TrainingAfrondenDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TrainingsDag))
            {
                return null;
            }

            var gebruikerDoel = await _repository.HaalGebruikerDoelOpAsync(gebruikerId);

            if (gebruikerDoel == null)
            {
                return null;
            }

            var voortgang = new TrainingVoortgang
            {
                gebruikerDoelId = gebruikerDoel.id,
                trainingsDag = dto.TrainingsDag,
                notitie = dto.Notitie,
                afgerondOp = DateTime.UtcNow
            };

            await _repository.VoegTrainingVoortgangToeAsync(voortgang);
            await _repository.SaveChangesAsync();

            return await GetDashboardAsync(gebruikerId);
        }

        private List<TrainingDagDto> MaakTrainingsschema(string doelNaam)
        {
            var doel = doelNaam.ToLower();

            if (doel.Contains("afvallen"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Full body kracht", Oefeningen = new() { "Squat", "Push-ups", "Lat pulldown", "Plank" } },
                    new() { Dag = "Dag 2", Titel = "Cardio", Oefeningen = new() { "30 minuten fietsen", "Interval wandelen", "Core training" } },
                    new() { Dag = "Dag 3", Titel = "Conditie en kracht", Oefeningen = new() { "Leg press", "Chest press", "Roeimachine", "Mountain climbers" } }
                };
            }

            if (doel.Contains("gezond"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Cardio basis", Oefeningen = new() { "20 minuten hardlopen", "Roeien", "Stretching" } },
                    new() { Dag = "Dag 2", Titel = "Intervaltraining", Oefeningen = new() { "Sprint intervallen", "Fietsen", "Core stability" } },
                    new() { Dag = "Dag 3", Titel = "Functionele training", Oefeningen = new() { "Kettlebell swings", "Burpees", "Battle ropes" } }
                };
            }

            if (doel.Contains("spiermassa opbouwen"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Krachttraining", Oefeningen = new() { "Squat", "Deadlift", "Bench press" } },
                    new() { Dag = "Dag 2", Titel = "Techniektraining", Oefeningen = new() { "Kickbokstechniek", "Voetenwerk", "Zaktraining" } },
                    new() { Dag = "Dag 3", Titel = "Conditie", Oefeningen = new() { "Interval cardio", "Rope skipping", "Core training" } },
                    new() { Dag = "Dag 4", Titel = "Hersteltraining", Oefeningen = new() { "Mobiliteit", "Lichte cardio", "Stretching" } }
                };
            }

            return new List<TrainingDagDto>
            {
                new() { Dag = "Dag 1", Titel = "Bovenlichaam", Oefeningen = new() { "Bench press", "Barbell row", "Overhead press", "Lat pulldown" } },
                new() { Dag = "Dag 2", Titel = "Onderlichaam", Oefeningen = new() { "Squat", "Romanian deadlift", "Leg press", "Seated leg curl" } },
                new() { Dag = "Dag 3", Titel = "Push training", Oefeningen = new() { "Incline press", "Shoulder press", "Triceps pushdown" } },
                new() { Dag = "Dag 4", Titel = "Pull training", Oefeningen = new() { "Pull-ups", "Cable row", "Bicep curl", "Face pulls" } }
            };
        }
    }
}