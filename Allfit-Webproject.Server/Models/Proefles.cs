namespace Allfit_Webproject.Server.Models
{
    public class Proefles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
        public int SportschoolID { get; set; }
        public Sportschool Sportschool { get; set; }
        public int LesID { get; set; }
        public Aanbod Les { get; set; }
    }
}
