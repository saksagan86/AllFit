namespace Allfit_Webproject.Server.Dtos
{
    public class IntakeDTO
    {
        public required String Naam { get; set; }
        public required String Email { get; set; }
        public required String Telefoon { get; set; }
        public String? Bericht { get; set; } = null;
    }
}
