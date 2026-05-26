namespace Allfit_Webproject.Server.Dtos
{
    public class ContactFormulierResponseDto
    {
        public int Id { get; set; }
        public string Bericht { get; set; } = string.Empty;
        public DateTime AangemaaktOp { get; set; }
    }
}