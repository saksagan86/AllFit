using Microsoft.AspNetCore.Mvc;
using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;
using Mollie;
using Mollie.Models.Components;
using Mollie.Models.Requests;


namespace Allfit_Webproject.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly String MollieAPIKey = "test_nQaGWn8wW86xzwGkr7DSCeBtQ8TzkA";

        [HttpPost("request")]
        public async Task<IActionResult> RequestAsync([FromBody] PaymentRequestDto paymentInfo)
        {
            var sdk = new Mollie.Client(security: new Security()
            {
                ApiKey = MollieAPIKey,
            });

            //var res = await sdk.Oauth.GenerateAsync(
            //    idempotencyKey: "123e4567-e89b-12d3-a456-426",
            //    requestBody: new OauthGenerateTokensRequestBody()
            //    {
            //        GrantType = OauthGrantType.AuthorizationCode,
            //        Code = "auth_...",
            //        RefreshToken = "refresh_...",
            //        RedirectUri = "https://example.com/redirect",
            //    }
            //);
            var paymentRequest = new PaymentRequest()
            {
                Description = "Desc",
                Amount = new Amount()
                {
                    Currency = "EUR",
                    Value = paymentInfo.Amount,
                },
                RedirectUrl = "http://localhost:5173/login",
                CancelUrl = "http://localhost:5173/inschrijven"
            };

            var payment1 = await sdk.Payments.CreateAsync(
                paymentRequest: paymentRequest
            );

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
