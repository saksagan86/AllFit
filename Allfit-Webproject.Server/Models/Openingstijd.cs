namespace Allfit_Webproject.Server.Models
{
    public class Openingstijd {
        public int id { get; set; }

        public string dag { get; set; }

        public string tijdOpen { get; set; }
        public string tijdSluit { get; set; }

        public int SportschoolId { get; set; }

        public Sportschool Sportschool { get; set; }
    }
}
