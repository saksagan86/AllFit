namespace Allfit_Webproject.Server.Dtos
{
    public class CoachingDashboardDto
    {
        public bool HeeftDoel { get; set; }
        public CoachingDoelDto? Doel { get; set; }
        public List<CoachingDoelDto> BeschikbareDoelen { get; set; } = new();
        public List<TrainingDagDto> Trainingsschema { get; set; } = new();
        public ProgressDto Progress { get; set; } = new();
        public List<TrainingHistorieDto> Historie { get; set; } = new();
    }

    public class CoachingDoelDto
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public string Beschrijving { get; set; } = string.Empty;
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