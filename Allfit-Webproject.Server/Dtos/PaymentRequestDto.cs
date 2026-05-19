namespace Allfit_Webproject.Server.Dtos
{
    public class PaymentRequestDto
    {
        public String Amount { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
        public String Description { get; set; } = string.Empty;
        public int LidId { get; set; }
        public int LidmaatschapId { get; set; }
    }
}
