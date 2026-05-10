namespace Allfit_Webproject.Server.Dtos
{
    public class GroepslesDTO
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Image { get; set; }

        public string Niveau { get; set; }

        public string Duur { get; set; }

        public bool ExtraBegeleiding { get; set; }

        public string? BeschrijvingBegeleiding { get; set; }
    }
}
