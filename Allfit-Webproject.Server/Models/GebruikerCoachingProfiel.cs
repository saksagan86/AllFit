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

        // Huidige/latest gewicht
        public decimal gewichtKg { get; set; }

        // Lange termijn doel
        public decimal? startGewichtKg { get; set; }
        public decimal? doelGewichtKg { get; set; }

        public DateTime? startDatum { get; set; }
        public DateTime? eindDatum { get; set; }

        public bool doelAfgerond { get; set; } = false;
        public bool? doelBehaald { get; set; }
        public string? evaluatieTekst { get; set; }

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