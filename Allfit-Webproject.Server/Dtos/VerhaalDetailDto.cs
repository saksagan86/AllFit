namespace Allfit_Webproject.Server.Dtos
{
    public class VerhaalDetailDto
    {

        public int Id { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public List<string> Fotos { get; set; }
        public DateTime GeplaatstOp { get; set; }
        public string GebruikersNaam { get; set; }
        public string AanbodType { get; set; }

    }
}
