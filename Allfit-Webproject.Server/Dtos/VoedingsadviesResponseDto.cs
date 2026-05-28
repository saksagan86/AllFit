namespace Allfit_Webproject.Server.Dtos
{
    public class MijnVoedingsschemaResponseDto
    {
        public bool HeeftDoel { get; set; }
        public List<BeschikbaarVoedingsDoelDto> BeschikbareDoelen { get; set; } = new();
        public BeschikbaarVoedingsDoelDto? Doel { get; set; }
        public VoedingsschemaDto? Schema { get; set; }
        public string? Message { get; set; }
    }

    public class BeschikbaarVoedingsDoelDto
    {
        public int Id { get; set; }
        public string Naam { get; set; } = string.Empty;
        public string Beschrijving { get; set; } = string.Empty;
    }

    public class VoedingsschemaDto
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Beschrijving { get; set; } = string.Empty;
        public List<VoedingsschemaRegelDto> Regels { get; set; } = new();
    }

    public class VoedingsschemaRegelDto
    {
        public int Id { get; set; }
        public string MaaltijdMoment { get; set; } = string.Empty;
        public string Advies { get; set; } = string.Empty;
        public int Volgorde { get; set; }
    }
}