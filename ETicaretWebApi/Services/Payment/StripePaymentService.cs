using Stripe;

namespace ETicaretWebApi.Services.Payment
{
    public class StripePaymentService : IPaymentService
    {
        public bool Processing(PaymentProcessingInfo processingInfo)
        {
            var optionsCharge = new ChargeCreateOptions
            {
                Amount = processingInfo.Amount,
                Currency = processingInfo.Currency,
                Description = processingInfo.Description,
                Source = processingInfo.Token,
                ReceiptEmail = processingInfo.CustomerEmail
            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status == "succeeded")
            {
                return true;
            }
            return false;
        }
    }
}
