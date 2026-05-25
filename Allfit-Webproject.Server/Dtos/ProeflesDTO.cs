using Allfit_Webproject.Server.Models;
using System.ComponentModel.DataAnnotations;
namespace Allfit_Webproject.Server.Dtos
{
    public class ProeflesDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
        [Required]
        public int SportschoolID { get; set; }
        [Required]
        public int LesID { get; set; }
    }
}
