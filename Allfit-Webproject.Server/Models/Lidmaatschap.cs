namespace Allfit_Webproject.Server.Models
{
    public class Lidmaatschap
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Type { get; set; }
        public double Inschrijfgeld { get; set; }
        public double Lidmaatschapgeld { get; set; }
        public string Duur { get; set; }
        public string Opzegtermijn { get; set; }
        public string Beschrijving { get; set; }

    }
}
