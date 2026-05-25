namespace Allfit_Webproject.Server.Dtos
{
    public class LesDto
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Tijd { get; set; }
        public int MaxDeelnemers { get; set; }
        public int VrijePlekken { get; set; }
        public bool IsIngeschreven { get; set; }
    }
}
