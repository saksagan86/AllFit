namespace Allfit_Webproject.Server.Models
{
    public class AdviesTemplate
    {
        public int id { get; set; }

        public int doelId { get; set; }
        public Doel? doel { get; set; }

        public string activiteitniveau { get; set; } = string.Empty;
        public string bmiCategorie { get; set; } = string.Empty;

        public string titel { get; set; } = string.Empty;
        public string beschrijving { get; set; } = string.Empty;

        public string calorieAdvies { get; set; } = string.Empty;
        public string eiwitAdvies { get; set; } = string.Empty;
        public string algemeneTips { get; set; } = string.Empty;

        public bool actief { get; set; } = true;
    }
}