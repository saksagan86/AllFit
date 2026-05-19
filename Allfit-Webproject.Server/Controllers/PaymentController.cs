using Microsoft.AspNetCore.Mvc;
using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;
using Mollie;
using Mollie.Models.Components;
using Mollie.Models.Requests;
using Allfit_Webproject.Server.Repository;


namespace Allfit_Webproject.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IAbonnementService _service;
        private readonly String MollieAPIKey = "test_nQaGWn8wW86xzwGkr7DSCeBtQ8TzkA";

        public PaymentController(IAbonnementService service)
        {
            _service = service;
        }

        // Wanneer een payment change is bij mollie wordt dit gecalled. Echter unreachable door gebruik van localhost
        [HttpPost("update")]
        public async Task StatusUpdate([FromBody] String mollieID)
        {
            var lid = await _service.GetLid(mollieID);
            var sdk = new Mollie.Client(security: new Security()
            {
                ApiKey = MollieAPIKey,
            });
            var paymentReq = new GetPaymentRequest()
            {
                PaymentId = mollieID
            };
            var payment = await sdk.Payments.GetAsync(paymentReq);
            Console.WriteLine(payment.PaymentResponse.Status);
            if (payment.PaymentResponse.Status == "paid")
            {
                Console.WriteLine("Order is paid");
                lid.isActief = true;
                await _service.UpdateLidAsync(lid);
            }
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestAsync([FromBody] PaymentRequestDto paymentInfo)
        {
            var sdk = new Mollie.Client(security: new Security()
            {
                ApiKey = MollieAPIKey,
            });

            var paymentRequest = new PaymentRequest()
            {
                Description = paymentInfo.Description,
                Amount = new Amount()
                {
                    Currency = "EUR",
                    Value = paymentInfo.Amount,
                },
                RedirectUrl = "http://localhost:5173/login",
                CancelUrl = "http://localhost:5173/inschrijven",
                //WebhookUrl = "http://localhost:7093/api/Payment/update"
            };

            var payment1 = await sdk.Payments.CreateAsync(
                paymentRequest: paymentRequest
            );

            try
            {
                AbonnementDTO abonnementDTO = new AbonnementDTO();
                abonnementDTO.LidID = paymentInfo.LidId;
                abonnementDTO.LidmaatschapID = paymentInfo.LidmaatschapId;
                abonnementDTO.MollieID = payment1.PaymentResponse.Id;
                abonnementDTO.DateStart = DateOnly.FromDateTime(DateTime.Today);
                abonnementDTO.DateEnd = abonnementDTO.DateStart.AddMonths(paymentInfo.Duration);
                await _service.RegisterAbonnementAsync(abonnementDTO);
            }
            catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
            
            return Ok(new
            {
                req = new
                {
                    payment1.PaymentResponse
                }
            }
                );
        }
    }
}
