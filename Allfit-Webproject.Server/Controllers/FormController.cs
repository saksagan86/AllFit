using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _service;

        public FormController(IFormService formService)
        {
            _service = formService;
        }

        [HttpPost("submit")]
        public async Task SubmitForm([FromBody] IntakeDTO formData)
        {
            // Write to database
            await _service.SaveFormAsync(formData);

            // Send confirmation email

            Console.WriteLine(formData.Naam);
        }
    }
}
