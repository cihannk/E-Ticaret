namespace ETicaretWebApi.Services.Payment
{
    public class PaymentProcessingInfo
    {
        public string Token { get; set; }
        public string CustomerEmail { get; set; }
        public string Name { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
