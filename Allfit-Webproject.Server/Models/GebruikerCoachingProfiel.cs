namespace Allfit_Webproject.Server.Models
{
    public class GebruikerCoachingProfiel
    {
        public int id { get; set; }

        public int gebruikerId { get; set; }
        public Gebruiker? gebruiker { get; set; }

        public int doelId { get; set; }
        public Doel? doel { get; set; }

        public int leeftijd { get; set; }
        public decimal lengteCm { get; set; }
        public decimal gewichtKg { get; set; }

        public string activiteitniveau { get; set; } = string.Empty;

        public int doelTermijnMaanden { get; set; } = 6;

        public decimal bmi { get; set; }
        public string bmiCategorie { get; set; } = string.Empty;

        public int? adviesTemplateId { get; set; }
        public AdviesTemplate? adviesTemplate { get; set; }

        public DateTime aangemaaktOp { get; set; } = DateTime.UtcNow;
        public DateTime gewijzigdOp { get; set; } = DateTime.UtcNow;
    }
}