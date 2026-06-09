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


            var weekRecords = profiel == null
                ? new List<WekelijkseVoortgang>()
                : await _repository.HaalWeekHistorieOpAsync(profiel.id);

            var weekHistorie = weekRecords.Any()
                ? MapWeekHistorie(weekRecords)
                : MaakWeekHistorie(alleHistorie, weekDoel);

            var maandHistorie = MaakMaandHistorie(weekHistorie);

            var langeTermijnEvaluatie = profiel == null
                ? null
                : BerekenLangeTermijnEvaluatie(
                    profiel,
                    gebruikerDoel.doel.naam,
                    weekRecords
                );
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
                    StartGewichtKg = profiel.startGewichtKg,
                    DoelGewichtKg = profiel.doelGewichtKg,
                    StartDatum = profiel.startDatum,
                    EindDatum = profiel.eindDatum,
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

               WeekHistorie = weekHistorie,
               MaandHistorie = maandHistorie,
               LangeTermijnEvaluatie = langeTermijnEvaluatie
            };
        }

        public async Task<CoachingDashboardDto?> KiesDoelAsync(int gebruikerId, KiesCoachingDoelDto dto)
        {
            var doelBestaat = await _repository.DoelBestaatEnIsActiefAsync(dto.DoelId);

            if (!doelBestaat)
            {
                return null;
            }

            var gebruikerDoel = await _repository.HaalGebruikerDoelMetDoelOpAsync(gebruikerId);
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

            var termijn = dto.DoelTermijnMaanden <= 0 ? 6 : dto.DoelTermijnMaanden;
            var doelGewijzigd = profiel.doelId != 0 && profiel.doelId != dto.DoelId;

            if (profiel.startDatum == null || profiel.startGewichtKg == null || doelGewijzigd)
            {
                profiel.startDatum = DateTime.UtcNow;
                profiel.startGewichtKg = dto.GewichtKg;
            }

            profiel.eindDatum = profiel.startDatum.Value.AddMonths(termijn);

            profiel.doelId = dto.DoelId;
            profiel.leeftijd = dto.Leeftijd;
            profiel.lengteCm = dto.LengteCm;
            profiel.gewichtKg = dto.GewichtKg;
            profiel.doelGewichtKg = dto.DoelGewichtKg;
            profiel.activiteitniveau = activiteitniveau;
            profiel.doelTermijnMaanden = termijn;
            profiel.bmi = bmi;
            profiel.bmiCategorie = bmiCategorie;
            profiel.adviesTemplateId = adviesTemplate?.id;
            profiel.doelAfgerond = false;
            profiel.doelBehaald = null;
            profiel.evaluatieTekst = null;
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

            var profiel = await _repository.HaalCoachingProfielOpAsync(gebruikerId);
            var activiteitniveau = profiel?.activiteitniveau ?? "Gemiddeld";

            var trainingsschema = MaakTrainingsschema(
                gebruikerDoel.doel?.naam ?? "",
                activiteitniveau
            );

            await UpdateWekelijkseVoortgangAsync(
                gebruikerId,
                gebruikerDoel,
                trainingsschema.Count
            );

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
        public async Task<CoachingDashboardDto?> SlaWeekVoortgangOpAsync(
            int gebruikerId,
            WeekVoortgangOpslaanDto dto
        )
        {
            var profiel = await _repository.HaalCoachingProfielOpAsync(gebruikerId);
            var gebruikerDoel = await _repository.HaalGebruikerDoelMetDoelOpAsync(gebruikerId);

            if (profiel == null || gebruikerDoel == null)
            {
                return null;
            }

            var weekStartDatum = BepaalWeekStart(DateTime.UtcNow);

            var weekVoortgang = await _repository.HaalWeekVoortgangOpAsync(
                profiel.id,
                weekStartDatum
            );

            if (weekVoortgang == null)
            {
                var trainingsschema = MaakTrainingsschema(
                    gebruikerDoel.doel?.naam ?? "",
                    profiel.activiteitniveau
                );

                weekVoortgang = new WekelijkseVoortgang
                {
                    gebruikerCoachingProfielId = profiel.id,
                    weekStartDatum = weekStartDatum,
                    weekDoel = trainingsschema.Count,
                    afgerondeTrainingen = 0,
                    doelBehaald = false,
                    aangemaaktOp = DateTime.UtcNow
                };

                await _repository.VoegWekelijkseVoortgangToeAsync(weekVoortgang);
            }

            weekVoortgang.gewichtKg = dto.GewichtKg;
            weekVoortgang.notitie = dto.Notitie;
            weekVoortgang.gewijzigdOp = DateTime.UtcNow;

            if (dto.GewichtKg.HasValue)
            {
                profiel.gewichtKg = dto.GewichtKg.Value;
                profiel.gewijzigdOp = DateTime.UtcNow;
            }

            await _repository.SaveChangesAsync();

            return await GetDashboardAsync(gebruikerId);
        }

        private async Task UpdateWekelijkseVoortgangAsync(
            int gebruikerId,
            GebruikerDoel gebruikerDoel,
            int weekDoel
        )
        {
            var profiel = await _repository.HaalCoachingProfielOpAsync(gebruikerId);

            if (profiel == null)
            {
                return;
            }

            var weekStartDatum = BepaalWeekStart(DateTime.UtcNow);
            var alleHistorie = await _repository.HaalAlleHistorieOpAsync(gebruikerDoel.id);

            var afgerondeTrainingenWerkelijk = alleHistorie
                .Where(h => BepaalWeekStart(h.afgerondOp) == weekStartDatum)
                .Select(h => h.trainingsDag)
                .Distinct()
                .Count();

            var afgerondeTrainingen = Math.Min(afgerondeTrainingenWerkelijk, weekDoel);

            var weekVoortgang = await _repository.HaalWeekVoortgangOpAsync(
                profiel.id,
                weekStartDatum
            );

            if (weekVoortgang == null)
            {
                weekVoortgang = new WekelijkseVoortgang
                {
                    gebruikerCoachingProfielId = profiel.id,
                    weekStartDatum = weekStartDatum,
                    gewichtKg = profiel.gewichtKg,
                    aangemaaktOp = DateTime.UtcNow
                };

                await _repository.VoegWekelijkseVoortgangToeAsync(weekVoortgang);
            }

            weekVoortgang.afgerondeTrainingen = afgerondeTrainingen;
            weekVoortgang.weekDoel = weekDoel;
            weekVoortgang.doelBehaald = weekDoel > 0 && afgerondeTrainingen >= weekDoel;
            weekVoortgang.gewijzigdOp = DateTime.UtcNow;
        }

        private List<WeekVoortgangDto> MapWeekHistorie(
            List<WekelijkseVoortgang> weekHistorie
        )
        {
            return weekHistorie.Select(wv =>
            {
                var percentage = wv.weekDoel == 0
                    ? 0
                    : Math.Min(
                        100,
                        (int)Math.Round((double)wv.afgerondeTrainingen / wv.weekDoel * 100)
                    );

                return new WeekVoortgangDto
                {
                    WeekStartDatum = wv.weekStartDatum,
                    GewichtKg = wv.gewichtKg,
                    AfgerondeTrainingen = wv.afgerondeTrainingen,
                    WeekDoel = wv.weekDoel,
                    DoelBehaald = wv.doelBehaald,
                    Percentage = percentage,
                    StatusTekst = MaakStatusTekst(wv.afgerondeTrainingen, wv.weekDoel),
                    Notitie = wv.notitie
                };
            }).ToList();
        }

        private List<MaandVoortgangDto> MaakMaandHistorie(
            List<WeekVoortgangDto> weekHistorie
        )
        {
            return weekHistorie
                .GroupBy(w => new
                {
                    w.WeekStartDatum.Year,
                    w.WeekStartDatum.Month
                })
                .OrderByDescending(g => g.Key.Year)
                .ThenByDescending(g => g.Key.Month)
                .Take(6)
                .Select(g =>
                {
                    var afgerond = g.Sum(x => x.AfgerondeTrainingen);
                    var doel = g.Sum(x => x.WeekDoel);

                    var percentage = doel == 0
                        ? 0
                        : Math.Min(100, (int)Math.Round((double)afgerond / doel * 100));

                    var gewichten = g
                        .Where(x => x.GewichtKg.HasValue)
                        .Select(x => x.GewichtKg!.Value)
                        .ToList();

                    return new MaandVoortgangDto
                    {
                        Maand = $"{g.Key.Month:D2}-{g.Key.Year}",
                        AantalWeken = g.Count(),
                        AfgerondeTrainingen = afgerond,
                        WeekDoelTotaal = doel,
                        Percentage = percentage,
                        StatusTekst = percentage >= 80
                            ? "Goed op schema"
                            : percentage >= 50
                                ? "Redelijk op schema"
                                : "Achter op schema",
                        GemiddeldGewichtKg = gewichten.Any()
                            ? Math.Round(gewichten.Average(), 1)
                            : null
                    };
                })
                .ToList();
        }

        private LangeTermijnEvaluatieDto BerekenLangeTermijnEvaluatie(
            GebruikerCoachingProfiel profiel,
            string doelNaam,
            List<WekelijkseVoortgang> weken
        )
        {
            var startGewicht = profiel.startGewichtKg ?? profiel.gewichtKg;
            var huidigGewicht = weken
                .Where(w => w.gewichtKg.HasValue)
                .OrderByDescending(w => w.weekStartDatum)
                .FirstOrDefault()
                ?.gewichtKg ?? profiel.gewichtKg;

            var doelGewicht = profiel.doelGewichtKg;
            var totaalWeken = weken.Count;
            var wekenBehaald = weken.Count(w => w.doelBehaald);

            var consistentie = totaalWeken == 0
                ? 0
                : (int)Math.Round((double)wekenBehaald / totaalWeken * 100);

            var eindDatum = profiel.eindDatum;
            var isEindDatumBereikt = eindDatum.HasValue && DateTime.UtcNow.Date >= eindDatum.Value.Date;
            var dagenTotEinddatum = eindDatum.HasValue
                ? Math.Max(0, (eindDatum.Value.Date - DateTime.UtcNow.Date).Days)
                : 0;

            var doel = doelNaam.ToLowerInvariant();
            bool? doelBehaald = null;

            if (doel.Contains("afvallen") && doelGewicht.HasValue)
            {
                doelBehaald = huidigGewicht <= doelGewicht.Value;
            }
            else if ((doel.Contains("aankomen") || doel.Contains("spier")) && doelGewicht.HasValue)
            {
                doelBehaald = huidigGewicht >= doelGewicht.Value && consistentie >= 70;
            }
            else if (doel.Contains("gewicht") || doel.Contains("gezond"))
            {
                doelBehaald = Math.Abs(huidigGewicht - startGewicht) <= 2;
            }

            var analyse = MaakLangeTermijnAnalyseTekst(
                doelBehaald,
                doelNaam,
                startGewicht,
                huidigGewicht,
                doelGewicht,
                consistentie,
                totaalWeken,
                wekenBehaald,
                isEindDatumBereikt
            );

            return new LangeTermijnEvaluatieDto
            {
                KanEvalueren = totaalWeken > 0,
                IsEindDatumBereikt = isEindDatumBereikt,
                DoelBehaald = doelBehaald,
                Status = doelBehaald == true
                    ? "Doel behaald"
                    : doelBehaald == false
                        ? "Nog niet behaald"
                        : "Nog niet te beoordelen",
                AnalyseTekst = analyse,
                StartGewichtKg = startGewicht,
                HuidigGewichtKg = huidigGewicht,
                DoelGewichtKg = doelGewicht,
                TrainingsConsistentiePercentage = consistentie,
                AantalWeken = totaalWeken,
                WekenDoelBehaald = wekenBehaald,
                DagenTotEinddatum = dagenTotEinddatum
            };
        }

        private string MaakLangeTermijnAnalyseTekst(
            bool? doelBehaald,
            string doelNaam,
            decimal startGewicht,
            decimal huidigGewicht,
            decimal? doelGewicht,
            int consistentie,
            int totaalWeken,
            int wekenBehaald,
            bool isEindDatumBereikt
        )
        {
            if (totaalWeken == 0)
            {
                return "Er zijn nog geen wekelijkse voortgangsgegevens. Rond trainingen af en vul wekelijks je gewicht in om je langetermijndoel beter te kunnen beoordelen.";
            }

            var periodeTekst = isEindDatumBereikt
                ? "De einddatum is bereikt."
                : "De einddatum is nog niet bereikt, dit is een tussenstand.";

            var basis =
                $"{periodeTekst} Je hebt in {wekenBehaald} van de {totaalWeken} weken je trainingsdoel gehaald. " +
                $"Je trainingsconsistentie is {consistentie}%.";

            if (doelBehaald == true)
            {
                return basis + " Op basis van je huidige gegevens lig je goed op schema of heb je je doel behaald.";
            }

            if (consistentie < 50)
            {
                return basis + " Je hebt je doel waarschijnlijk nog niet gehaald omdat je in veel weken onder je trainingsdoel zat.";
            }

            if (doelGewicht.HasValue)
            {
                return basis + $" Je huidige gewicht is {huidigGewicht} kg en je doelgewicht is {doelGewicht.Value} kg. Je bent dus nog niet volledig bij je doelgewicht.";
            }

            return basis + " Er is nog niet genoeg doelinformatie om een definitieve conclusie te trekken.";
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