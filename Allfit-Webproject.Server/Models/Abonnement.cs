using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Models
{
    [PrimaryKey(nameof(LidID), nameof(MollieID))]
    public class Abonnement
    {
        public int LidID { get; set; }
        public int LidmaatschapID { get; set; }
        public String MollieID { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
    }
}
