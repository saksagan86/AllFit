namespace Allfit_Webproject.Server.Models
{
    public abstract class Gebruiker
    {
        public int id { get; set; }
        public string naam { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string wachtwoord { get; set; } = string.Empty;
        public string telefoonnummer { get; set; } = string.Empty;

    }
}
