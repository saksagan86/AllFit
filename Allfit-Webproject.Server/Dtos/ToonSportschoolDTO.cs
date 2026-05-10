namespace Allfit_Webproject.Server.Dtos
{
    public class ToonSportschoolDTO
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Stad { get; set; }

        public string Adres { get; set; }

        public List<ToonOpeningstijdInfoDTO> Openingstijden { get; set; }

        public List<ToonFaciliteitInfoDTO> Faciliteiten { get; set; }
    }
}
