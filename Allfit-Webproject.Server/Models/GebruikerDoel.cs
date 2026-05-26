namespace Allfit_Webproject.Server.Models
{
    public class GebruikerDoel
    {
        public int id { get; set; }

        public int gebruikerId { get; set; }

        public Gebruiker? gebruiker { get; set; }

        public int doelId { get; set; }

        public Doel? doel { get; set; }

        public DateTime aangemaaktOp { get; set; } = DateTime.UtcNow;

        public DateTime gewijzigdOp { get; set; } = DateTime.UtcNow;
    }
}