using System.ComponentModel.DataAnnotations;
namespace Allfit_Webproject.Server.Dtos
{
    public class AanbodDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        public string SportType { get; set; }
    }
}
