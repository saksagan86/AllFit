namespace Allfit_Webproject.Server.Models
{
    public class Voedingsschema
    {
        public int id { get; set; }

        public int doelId { get; set; }

        public Doel? doel { get; set; }

        public string titel { get; set; } = string.Empty;

        public string beschrijving { get; set; } = string.Empty;

        public List<VoedingsschemaRegel> regels { get; set; } = new();
    }
}