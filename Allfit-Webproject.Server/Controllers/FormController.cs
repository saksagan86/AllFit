using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

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

        [HttpPost("submit")]
        public async Task SubmitForm([FromBody] IntakeDTO formData)
        {
            // Write to database
            await _service.SaveFormAsync(formData);

            // Send confirmation email wegens testomgeving alleen mogelijk naar r.p.j.welleman@student.hhs.nl
            try
            {
                MailMessage message = new MailMessage(
                    mailDomain,
                    formData.Email,
                    "Bevestiging van uw aanvraag",
                    $"Beste {formData.Naam},\n\nBedankt voor uw aanvraag. We nemen zo snel mogelijk telefonisch contact met u op via {formData.Telefoon}.\n\nMet vriendelijke groet,\nAllfit Team"
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
