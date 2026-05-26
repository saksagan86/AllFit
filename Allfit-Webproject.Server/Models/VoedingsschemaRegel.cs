namespace Allfit_Webproject.Server.Models
{
    public class VoedingsschemaRegel
    {
        public int id { get; set; }

        public int voedingsschemaId { get; set; }

        public Voedingsschema? voedingsschema { get; set; }

        public string maaltijdMoment { get; set; } = string.Empty;

        public string advies { get; set; } = string.Empty;

        public int volgorde { get; set; }
    }
}