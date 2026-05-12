namespace Allfit_Webproject.Server.Dtos
{
    public class PaymentRequestDto
    {
        public String Amount { get; set; } = string.Empty;
        public float RecurringAmount { get; set; } = 0;
    }
}
