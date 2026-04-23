using Microsoft.OpenApi.Models;

namespace Allfit_Webproject.Server.Models
{
    public class Sportschool
    {
        public int id { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string stad { get; set; }
        public string openingstijden { get; set; }
        //list alleTrainers
        public List<Faciliteit> alleFaciliteiten = new List<Faciliteit>();
        public List<Sportschool> Sportschools = new List<Sportschool>();
        //list alleFitnessApparatuur

    }
}
