namespace Allfit_Webproject.Server.Models
{
    public class Form
    {
        public int Id { get; set; }
        public required String Naam { get; set; }
        public required String Email { get; set; }
        public required String Telefoon { get; set; }
        public String? Bericht { get; set; }
    }
}
