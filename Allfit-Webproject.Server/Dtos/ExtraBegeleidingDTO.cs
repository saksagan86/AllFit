namespace Allfit_Webproject.Server.Dtos
{
    public class ExtraBegeleidingDTO
    {

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Image { get; set; }
        public string SportType { get; set; }
        public string BeschrijvingBegeleiding { get; set; }
        // Voor groepsles en kickboks
        public string? TrainerNaam { get; set; }
        // Groepsles
        public string? Niveau { get; set; }
        public string? Duur { get; set; }
        // Kickboks
        public string? Doelgroep { get; set; }
    }
}
