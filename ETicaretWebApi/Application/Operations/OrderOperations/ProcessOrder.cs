using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Services.Payment;
using ETicaretWebApi.Entitites;
using AutoMapper;

namespace ETicaretWebApi.Application.Operations.OrderOperations
{
    public class ProcessOrder
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;

        public PaymentModel Model { get; set; }

        public ProcessOrder(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(int userId)
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
    }
    public class PaymentModel
    {
        public string IntentId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
    }
}
