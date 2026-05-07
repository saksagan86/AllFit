namespace Allfit_Webproject.Server.Dtos;
using System.ComponentModel.DataAnnotations;

public class RegisterLidDTO
{
    [Required]
    public string Naam { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Wachtwoord { get; set; }

    [Required]
    public string BevestigWachtwoord { get; set; }

    [Required]
    public string Telefoonnummer { get; set; }

    [Required]
    public string Geboortedatum { get; set; }

    [Required]
    public string Adres { get; set; }

    [Required]
    public string Huisnummer { get; set; }

    [Required]
    public string Woonplaats { get; set; }

    [Required]
    public string Postcode { get; set; }

    public bool AccepteerVoorwaarden { get; set; }

    public int LidmaatschapId { get; set; }
}