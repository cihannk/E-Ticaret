namespace ETicaretWebApi.Services.Payment
{
    public interface IPaymentService
    {
        bool Processing(PaymentProcessingInfo processingInfo);
    }
}
