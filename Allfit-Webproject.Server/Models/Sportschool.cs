using Microsoft.OpenApi.Models;

namespace Allfit_Webproject.Server.Models
{
    public class Sportschool
    {
        public int id { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string stad { get; set; }
        public float lon { get; set; }
        public float lat { get; set; }
        public List<Openingstijd> openingstijdenSportschool { get; set; }
        //list alleTrainers
        public List<Faciliteit> alleFaciliteiten { get; set; }

    }
}
