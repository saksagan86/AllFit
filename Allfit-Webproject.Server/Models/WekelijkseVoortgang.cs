namespace Allfit_Webproject.Server.Models
{
    public class WekelijkseVoortgang
    {
        public int id { get; set; }

        public int gebruikerCoachingProfielId { get; set; }
        public GebruikerCoachingProfiel? gebruikerCoachingProfiel { get; set; }

        public DateTime weekStartDatum { get; set; }

        public decimal? gewichtKg { get; set; }

        public int afgerondeTrainingen { get; set; }
        public int weekDoel { get; set; }

        public bool doelBehaald { get; set; }

        public string? notitie { get; set; }

        public DateTime aangemaaktOp { get; set; } = DateTime.UtcNow;
        public DateTime gewijzigdOp { get; set; } = DateTime.UtcNow;
    }
}