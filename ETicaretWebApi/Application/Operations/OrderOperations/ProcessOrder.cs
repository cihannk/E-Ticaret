using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Services.Payment;
using ETicaretWebApi.Entitites;
using AutoMapper;

namespace ETicaretWebApi.Application.Operations.OrderOperations
{
    public class ProcessOrder
    {
        private readonly IPaymentService _paymentService;
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;

        public PaymentModel Model { get; set; }

        public ProcessOrder(IPaymentService paymentService, ETicaretDbContext context, IMapper mapper)
        {
            _paymentService = paymentService;
            _context = context;
            _mapper = mapper;
        }
        public void Handle(int userId)
        {
            var paymentResult = _paymentService.Processing(Model.PaymentProcessingInfo);
            if (paymentResult)
            {
                var cartItems = _mapper.Map<List<CartItem>>(Model.CartItems);
                var order = new Order
                {
                    CartItems = cartItems,
                    Date = DateTime.Now,
                    Total = cartItems.Sum(x => x.Amount * x.UnitPrice),
                    UserId = userId
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Ödeme başarısız oldu");
            }
        }
    }
    public class PaymentModel
    {
        public List<CartItemModel> CartItems { get; set; }
        public PaymentProcessingInfo PaymentProcessingInfo { get; set; }
    }
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
    }
}
