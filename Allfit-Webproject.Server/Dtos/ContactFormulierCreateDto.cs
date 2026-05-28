using System.ComponentModel.DataAnnotations;

namespace Allfit_Webproject.Server.Dtos
{
    public class ContactFormulierCreateDto
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naam moet tussen 2 en 100 tekens zijn.")]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [EmailAddress(ErrorMessage = "Vul een geldig e-mailadres in.")]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Onderwerp is verplicht.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Onderwerp moet tussen 3 en 150 tekens zijn.")]
        public string Titel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bericht is verplicht.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Bericht moet tussen 10 en 2000 tekens zijn.")]
        public string Beschrijving { get; set; } = string.Empty;

        // Honeypot veld tegen simpele bots. Normale gebruikers zien/vullen dit niet in.
        public string? Website { get; set; }
    }
}