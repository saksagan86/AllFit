namespace Allfit_Webproject.Server.Models
{
    public class Doel
    {
        public int id { get; set; }

        public string naam { get; set; } = string.Empty;

        public string beschrijving { get; set; } = string.Empty;

        public bool actief { get; set; } = true;

        public List<Voedingsschema> voedingsschemas { get; set; } = new();
    }
}