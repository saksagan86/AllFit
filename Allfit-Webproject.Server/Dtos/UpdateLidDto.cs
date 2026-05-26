using System.ComponentModel.DataAnnotations;

namespace Allfit_Webproject.Server.Dtos
{
    public class UpdateLidDto
    {
        [Required(ErrorMessage = "Naam invullen is verplicht")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "E-mailadres invullen is verplicht")]
        [EmailAddress(ErrorMessage = "Vul een geldig e-mailadres in")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefoonnummer invullen is verplicht")]
        public string Telefoonnummer { get; set; }

        [Required(ErrorMessage = "Adres invullen is verplicht")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Huisnummer invullen is verplicht")]
        public string Huisnummer { get; set; }

        [Required(ErrorMessage = "Postcode invullen is verplicht")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Stad invullen is verplicht")]
        public string Stad { get; set; }

    }
}
