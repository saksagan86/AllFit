namespace Allfit_Webproject.Server.Dtos
{
    public class CoachingDashboardDto
    {
        public bool HeeftDoel { get; set; }
        public bool HeeftProfiel { get; set; }

        public CoachingDoelDto? Doel { get; set; }
        public CoachingProfielDto? Profiel { get; set; }
        public AdviesTemplateDto? Advies { get; set; }

        public List<CoachingDoelDto> BeschikbareDoelen { get; set; } = new();
        public List<TrainingDagDto> Trainingsschema { get; set; } = new();
        public ProgressDto Progress { get; set; } = new();
        public List<TrainingHistorieDto> Historie { get; set; } = new();
        public List<WeekVoortgangDto> WeekHistorie { get; set; } = new();
    }

    public class CoachingDoelDto
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public string Beschrijving { get; set; } = string.Empty;
    }

    public class CoachingProfielDto
    {
        public int Id { get; set; }
        public int Leeftijd { get; set; }
        public decimal LengteCm { get; set; }
        public decimal GewichtKg { get; set; }
        public string Activiteitniveau { get; set; } = string.Empty;
        public int DoelTermijnMaanden { get; set; }
        public decimal Bmi { get; set; }
        public string BmiCategorie { get; set; } = string.Empty;
    }

    public class AdviesTemplateDto
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Beschrijving { get; set; } = string.Empty;
        public string CalorieAdvies { get; set; } = string.Empty;
        public string EiwitAdvies { get; set; } = string.Empty;
        public string AlgemeneTips { get; set; } = string.Empty;
    }

    public class TrainingDagDto
    {
        public string Dag { get; set; } = string.Empty;
        public string Titel { get; set; } = string.Empty;
        public List<string> Oefeningen { get; set; } = new();
    }

    public class ProgressDto
    {
        public int AfgerondDezeWeek { get; set; }
        public int WeekDoel { get; set; }
        public int Percentage { get; set; }
    }

    public class TrainingHistorieDto
    {
        public int Id { get; set; }
        public string TrainingsDag { get; set; } = string.Empty;
        public DateTime AfgerondOp { get; set; }
        public string? Notitie { get; set; }
    }

    public class WeekVoortgangDto
    {
        public DateTime WeekStartDatum { get; set; }
        public int AfgerondeTrainingen { get; set; }
        public int WeekDoel { get; set; }
        public bool DoelBehaald { get; set; }
        public int Percentage { get; set; }
        public string StatusTekst { get; set; } = string.Empty;
    }

    public class CoachingProfielAanvraagDto
    {
        public int DoelId { get; set; }
        public int Leeftijd { get; set; }
        public decimal LengteCm { get; set; }
        public decimal GewichtKg { get; set; }
        public string Activiteitniveau { get; set; } = string.Empty;
        public int DoelTermijnMaanden { get; set; } = 6;
    }

    public class KiesCoachingDoelDto
    {
        public int DoelId { get; set; }
    }

    public class TrainingAfrondenDto
    {
        public string TrainingsDag { get; set; } = string.Empty;
        public string? Notitie { get; set; }
    }
}