namespace Allfit_Webproject.Server.Models
{
    public class Inschrijving
    {

        public int Id { get; set; }
        public int LidId { get; set; }
        public Lid Lid { get; set; }
        public int LesId { get; set; }
        public Les Les { get; set; }
        public bool ExtraBegeleiding { get; set; }

    }
}
