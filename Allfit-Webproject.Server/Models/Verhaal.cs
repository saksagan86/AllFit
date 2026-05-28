namespace Allfit_Webproject.Server.Models
{
    public class Verhaal
    {

        public int Id { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public string Image { get; set; }
        public DateTime GeplaatstOp { get; set; }
        public int LidId { get; set; }
        public Lid Lid { get; set; }
        public int AanbodId { get; set; }
        public Aanbod Aanbod { get; set; }

    }
}
