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
            var profiel = await _repository.HaalCoachingProfielMetAllesOpAsync(gebruikerId);

            if (gebruikerDoel == null || gebruikerDoel.doel == null)
            {
                return new CoachingDashboardDto
                {
                    HeeftDoel = false,
                    HeeftProfiel = profiel != null,
                    BeschikbareDoelen = beschikbareDoelen
                };
            }

            var activiteitniveau = profiel?.activiteitniveau ?? "Gemiddeld";
            var trainingsschema = MaakTrainingsschema(gebruikerDoel.doel.naam, activiteitniveau);
            var weekDoel = trainingsschema.Count;

            var historie = await _repository.HaalHistorieOpAsync(gebruikerDoel.id);
            var alleHistorie = await _repository.HaalAlleHistorieOpAsync(gebruikerDoel.id);

            var startVanWeek = BepaalWeekStart(DateTime.UtcNow);

            var afgerondDezeWeekWerkelijk = alleHistorie
                .Where(h => BepaalWeekStart(h.afgerondOp) == startVanWeek)
                .Select(h => h.trainingsDag)
                .Distinct()
                .Count();

            var afgerondDezeWeek = Math.Min(afgerondDezeWeekWerkelijk, weekDoel);

            var percentage = weekDoel == 0
                ? 0
                : Math.Min(100, (int)Math.Round((double)afgerondDezeWeek / weekDoel * 100));

            return new CoachingDashboardDto
            {
                HeeftDoel = true,
                HeeftProfiel = profiel != null,

                BeschikbareDoelen = beschikbareDoelen,

                Doel = new CoachingDoelDto
                {
                    Id = gebruikerDoel.doel.id,
                    Naam = gebruikerDoel.doel.naam,
                    Beschrijving = gebruikerDoel.doel.beschrijving
                },

                Profiel = profiel == null ? null : new CoachingProfielDto
                {
                    Id = profiel.id,
                    Leeftijd = profiel.leeftijd,
                    LengteCm = profiel.lengteCm,
                    GewichtKg = profiel.gewichtKg,
                    Activiteitniveau = profiel.activiteitniveau,
                    DoelTermijnMaanden = profiel.doelTermijnMaanden,
                    Bmi = profiel.bmi,
                    BmiCategorie = profiel.bmiCategorie
                },

                Advies = profiel?.adviesTemplate == null ? null : new AdviesTemplateDto
                {
                    Id = profiel.adviesTemplate.id,
                    Titel = profiel.adviesTemplate.titel,
                    Beschrijving = profiel.adviesTemplate.beschrijving,
                    CalorieAdvies = profiel.adviesTemplate.calorieAdvies,
                    EiwitAdvies = profiel.adviesTemplate.eiwitAdvies,
                    AlgemeneTips = profiel.adviesTemplate.algemeneTips
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
                }).ToList(),

                WeekHistorie = MaakWeekHistorie(alleHistorie, weekDoel)
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

        public async Task<CoachingDashboardDto?> MaakOfUpdateProfielAsync(
            int gebruikerId,
            CoachingProfielAanvraagDto dto
        )
        {
            if (!IsProfielAanvraagGeldig(dto))
            {
                return null;
            }

            var doelBestaat = await _repository.DoelBestaatEnIsActiefAsync(dto.DoelId);

            if (!doelBestaat)
            {
                return null;
            }

            var activiteitniveau = NormaliseerActiviteitniveau(dto.Activiteitniveau);
            var bmi = BerekenBmi(dto.LengteCm, dto.GewichtKg);
            var bmiCategorie = BepaalBmiCategorie(bmi);

            var adviesTemplate = await _repository.ZoekAdviesTemplateAsync(
                dto.DoelId,
                activiteitniveau,
                bmiCategorie
            );

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

            var profiel = await _repository.HaalCoachingProfielOpAsync(gebruikerId);

            if (profiel == null)
            {
                profiel = new GebruikerCoachingProfiel
                {
                    gebruikerId = gebruikerId,
                    aangemaaktOp = DateTime.UtcNow
                };

                await _repository.VoegCoachingProfielToeAsync(profiel);
            }

            profiel.doelId = dto.DoelId;
            profiel.leeftijd = dto.Leeftijd;
            profiel.lengteCm = dto.LengteCm;
            profiel.gewichtKg = dto.GewichtKg;
            profiel.activiteitniveau = activiteitniveau;
            profiel.doelTermijnMaanden = dto.DoelTermijnMaanden <= 0
                ? 6
                : dto.DoelTermijnMaanden;
            profiel.bmi = bmi;
            profiel.bmiCategorie = bmiCategorie;
            profiel.adviesTemplateId = adviesTemplate?.id;
            profiel.gewijzigdOp = DateTime.UtcNow;

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

            var startVanWeek = BepaalWeekStart(DateTime.UtcNow);
            var trainingAlGedaan = await _repository.HeeftTrainingDezeWeekAsync(
                gebruikerDoel.id,
                dto.TrainingsDag,
                startVanWeek
            );

            if (trainingAlGedaan)
            {
                return await GetDashboardAsync(gebruikerId);
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

        private bool IsProfielAanvraagGeldig(CoachingProfielAanvraagDto dto)
        {
            if (dto.DoelId <= 0)
            {
                return false;
            }

            if (dto.Leeftijd < 12 || dto.Leeftijd > 100)
            {
                return false;
            }

            if (dto.LengteCm < 100 || dto.LengteCm > 250)
            {
                return false;
            }

            if (dto.GewichtKg < 30 || dto.GewichtKg > 250)
            {
                return false;
            }

            return true;
        }

        private decimal BerekenBmi(decimal lengteCm, decimal gewichtKg)
        {
            var lengteMeter = lengteCm / 100;

            if (lengteMeter <= 0)
            {
                return 0;
            }

            var bmi = gewichtKg / (lengteMeter * lengteMeter);
            return Math.Round(bmi, 1);
        }

        private string BepaalBmiCategorie(decimal bmi)
        {
            if (bmi < 18.5m)
            {
                return "Ondergewicht";
            }

            if (bmi < 25m)
            {
                return "Gezond gewicht";
            }

            if (bmi < 30m)
            {
                return "Overgewicht";
            }

            return "Ernstig overgewicht";
        }

        private string NormaliseerActiviteitniveau(string activiteitniveau)
        {
            var niveau = activiteitniveau.Trim().ToLowerInvariant();

            if (niveau.Contains("laag") || niveau.Contains("beginner"))
            {
                return "Laag";
            }

            if (niveau.Contains("hoog") || niveau.Contains("actief"))
            {
                return "Hoog";
            }

            return "Gemiddeld";
        }


        private DateTime BepaalWeekStart(DateTime datum)
        {
            var verschil = (7 + ((int)datum.DayOfWeek - (int)DayOfWeek.Monday)) % 7;
            return datum.Date.AddDays(-verschil);
        }

        private List<WeekVoortgangDto> MaakWeekHistorie(
            List<TrainingVoortgang> alleHistorie,
            int weekDoel
        )
        {
            return alleHistorie
                .GroupBy(h => BepaalWeekStart(h.afgerondOp))
                .OrderByDescending(g => g.Key)
                .Take(8)
                .Select(g =>
                {
                    var werkelijkAantal = g
                        .Select(x => x.trainingsDag)
                        .Distinct()
                        .Count();

                    var afgerondeTrainingen = Math.Min(werkelijkAantal, weekDoel);

                    var percentage = weekDoel == 0
                        ? 0
                        : Math.Min(100, (int)Math.Round((double)afgerondeTrainingen / weekDoel * 100));

                    return new WeekVoortgangDto
                    {
                        WeekStartDatum = g.Key,
                        AfgerondeTrainingen = afgerondeTrainingen,
                        WeekDoel = weekDoel,
                        DoelBehaald = afgerondeTrainingen >= weekDoel,
                        Percentage = percentage,
                        StatusTekst = MaakStatusTekst(afgerondeTrainingen, weekDoel)
                    };
                })
                .ToList();
        }

        private string MaakStatusTekst(int afgerondeTrainingen, int weekDoel)
        {
            if (weekDoel == 0)
            {
                return "Geen weekdoel ingesteld.";
            }

            if (afgerondeTrainingen >= weekDoel)
            {
                return "Doel behaald";
            }

            if (afgerondeTrainingen == weekDoel - 1)
            {
                return "Bijna gehaald";
            }

            if (afgerondeTrainingen == 0)
            {
                return "Nog geen trainingen afgerond";
            }

            return "Onder weekdoel";
        }

        private List<TrainingDagDto> MaakTrainingsschema(string doelNaam, string activiteitniveau)
        {
            var doel = doelNaam.ToLowerInvariant();
            var niveau = activiteitniveau.ToLowerInvariant();

            if (doel.Contains("afvallen"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Full body kracht", Oefeningen = new() { "Squat", "Push-ups", "Lat pulldown", "Plank" } },
                    new() { Dag = "Dag 2", Titel = "Cardio en core", Oefeningen = new() { "30 minuten fietsen", "Interval wandelen", "Core training" } },
                    new() { Dag = "Dag 3", Titel = "Conditie en kracht", Oefeningen = new() { "Leg press", "Chest press", "Roeimachine", "Mountain climbers" } }
                };
            }

            if (doel.Contains("aankomen"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Bovenlichaam kracht", Oefeningen = new() { "Bench press", "Seated row", "Shoulder press", "Bicep curl" } },
                    new() { Dag = "Dag 2", Titel = "Onderlichaam kracht", Oefeningen = new() { "Squat", "Leg press", "Romanian deadlift", "Calf raises" } },
                    new() { Dag = "Dag 3", Titel = "Full body kracht", Oefeningen = new() { "Deadlift", "Lat pulldown", "Chest press", "Plank" } }
                };
            }

            if (doel.Contains("spier"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Push training", Oefeningen = new() { "Bench press", "Incline press", "Shoulder press", "Triceps pushdown" } },
                    new() { Dag = "Dag 2", Titel = "Pull training", Oefeningen = new() { "Pull-ups", "Cable row", "Lat pulldown", "Bicep curl" } },
                    new() { Dag = "Dag 3", Titel = "Leg day", Oefeningen = new() { "Squat", "Leg press", "Romanian deadlift", "Leg curl" } },
                    new() { Dag = "Dag 4", Titel = "Full body", Oefeningen = new() { "Deadlift", "Dumbbell press", "Seated row", "Core training" } }
                };
            }

            if (doel.Contains("gewicht") || doel.Contains("gezond"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Algemene kracht", Oefeningen = new() { "Goblet squat", "Chest press", "Seated row", "Plank" } },
                    new() { Dag = "Dag 2", Titel = "Beweging en conditie", Oefeningen = new() { "20 minuten wandelen of fietsen", "Roeimachine licht tempo", "Mobiliteitsoefeningen" } },
                    new() { Dag = "Dag 3", Titel = "Full body onderhoud", Oefeningen = new() { "Leg press", "Shoulder press", "Lat pulldown", "Stretching" } }
                };
            }

            if (niveau.Contains("laag"))
            {
                return new List<TrainingDagDto>
                {
                    new() { Dag = "Dag 1", Titel = "Basis full body", Oefeningen = new() { "Leg press", "Chest press", "Seated row", "Plank" } },
                    new() { Dag = "Dag 2", Titel = "Lichte cardio", Oefeningen = new() { "20 minuten wandelen", "Fietsen", "Mobiliteit" } }
                };
            }

            return new List<TrainingDagDto>
            {
                new() { Dag = "Dag 1", Titel = "Algemene training", Oefeningen = new() { "Full body warming-up", "Basis krachttraining", "Lichte cardio", "Stretching" } }
            };
        }
    }
}