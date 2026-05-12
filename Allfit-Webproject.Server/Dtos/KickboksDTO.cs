namespace Allfit_Webproject.Server.Dtos
{
    public class KickboksDTO
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Image { get; set; }

        public string Doelgroep { get; set; }

        public bool ExtraBegeleiding { get; set; }

        public string? BeschrijvingBegeleiding { get; set; }
        public int? TrainerId { get; set; }
        public string TrainerNaam { get; set; }
    }
}
