using System.ComponentModel.DataAnnotations;

namespace Allfit_Webproject.Server.Dtos
{
    public class AbonnementDTO
    {
        [Required]
        public int LidID { get; set; }

        [Required]
        public int LidmaatschapID { get; set; }

        [Required]
        public String MollieID { get; set; }

        [Required]
        public DateOnly DateStart { get; set; }

        [Required]
        public DateOnly DateEnd { get; set; }
    }
}
