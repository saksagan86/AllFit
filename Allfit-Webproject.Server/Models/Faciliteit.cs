namespace Allfit_Webproject.Server.Models
{
    public class Faciliteit
    {
        public int id { get; set; }
        public string naam { get; set; }
        public string beschrijving { get; set; }
        public int SportschoolId { get; set; }
        public Sportschool Sportschool { get; set; }
    }
}
