namespace Allfit_Webproject.Server.Dtos
{
    public class VerhaalToevoegenDto
    {
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public int AanbodId { get; set; }
        public string Image { get; set; }
        public List<IFormFile>? Fotos { get; set; }
    }
}
