namespace Allfit_Webproject.Server.Models
{
    public class Lid: Gebruiker
    {
        public string geboortedatum { get; set; }
        public Boolean isActief { get; set; }
        public string adres { get; set; }
        public string huisnummer { get; set; }
        public string woonplaats { get; set; }
        public string postcode { get; set; }
        public int lidmaatschapId { get; set; }

    }
}
