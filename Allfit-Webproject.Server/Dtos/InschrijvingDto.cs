namespace Allfit_Webproject.Server.Dtos
{
    public class InschrijvingDto
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Tijd { get; set; }
        public string AanbodNaam { get; set; }
        public bool ExtraBegeleiding { get; set; }
    }
}
