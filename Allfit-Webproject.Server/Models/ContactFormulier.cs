using System.ComponentModel.DataAnnotations;

namespace Allfit_Webproject.Server.Models
{
    public class ContactFormulier
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string naam { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string email { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string titel { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string beschrijving { get; set; } = string.Empty;

        public DateTime aangemaaktOp { get; set; } = DateTime.UtcNow;

        [MaxLength(30)]
        public string status { get; set; } = "Nieuw";
    }
}