namespace Allfit_Webproject.Server.Models
{
    public class Les
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Tijd { get; set; }
        public int MaxDeelnemers { get; set; }
        public int AanbodId { get; set; }
        public Aanbod Aanbod { get; set; }

    }
}
