using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Net;
using System.Net.Mail;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _service;
        private readonly String mailTrapToken = "8ebfb0ad5d1ac972bd0ed249699cc57e";
        private readonly String mailDomain = "hello@demomailtrap.co";

        public FormController(IFormService formService)
        {
            _service = formService;
        }

        [HttpPost("proefles")]
        public async Task SubmitProefles([FromBody] ProeflesDTO formData)
        {
            // Write to database
            try
            {
                await _service.SaveProeflesAsync(formData);
                SendConfirmationEmail(formData.Email, formData.Name, formData.Telefoon);
            }
            catch (Exception ex) { 
                Console.Error.WriteLine(ex.Message);
            }
        }

        [HttpPost("submit")]
        public async Task SubmitForm([FromBody] IntakeDTO formData)
        {
            // Write to database
            try
            {
                await _service.SaveFormAsync(formData);
                SendConfirmationEmail(formData.Email, formData.Naam, formData.Telefoon);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private void SendConfirmationEmail(String email, String naam, String telefoon)
        {
            // Send confirmation email wegens testomgeving alleen mogelijk naar r.p.j.welleman@student.hhs.nl
            try
            {
                MailMessage message = new MailMessage(
                    mailDomain,
                    email,
                    "Bevestiging van uw aanvraag",
                    $"Beste {naam},\n\nBedankt voor uw aanvraag. We nemen zo snel mogelijk telefonisch contact met u op via {telefoon}.\n\nMet vriendelijke groet,\nAllfit Team"
                    );
                SmtpClient client = new SmtpClient("live.smtp.mailtrap.io", 587)
                {
                    Credentials = new NetworkCredential("api", mailTrapToken),
                    EnableSsl = true
                };
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
