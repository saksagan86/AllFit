namespace Allfit_Webproject.Server.Models
{
    public class TrainingVoortgang
    {
        public int id { get; set; }

        public int gebruikerDoelId { get; set; }
        public GebruikerDoel? gebruikerDoel { get; set; }

        public string trainingsDag { get; set; } = string.Empty;

        public DateTime afgerondOp { get; set; } = DateTime.UtcNow;

        public string? notitie { get; set; }
    }
}