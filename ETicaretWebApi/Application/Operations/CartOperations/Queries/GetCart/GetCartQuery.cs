
using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.CartOperations.Queries.GetCartItem;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Queries.GetCart
{
    public class GetCartQuery : IQuery<GetCartViewModel>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public int UserId { get; set; }

        public GetCartQuery(IMapper mapper, ETicaretDbContext context)
        {
             _context = context;
            _mapper = mapper;
        }
        public GetCartViewModel Handle()
        {
            var cart = _context.Carts.FirstOrDefault(x => x.UserId == UserId);
            if (cart is null)
                throw new InvalidOperationException("Cart is not exist");

            var cartCartItems = _context.CartCartItems.Where(x => x.CartId == cart.Id).ToList();
            List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
            foreach (var cartItem in cartCartItems)
            {
                var cartItemToAppend = _context.CartItems.SingleOrDefault(x => x.Id == cartItem.CartItemId);
                if (cartItemToAppend is null)
                    throw new InvalidOperationException("CartItem is empty");
                else
                {
                    GetCartItemQuery query = new GetCartItemQuery(_context);
                    query.CartItemId = cartItem.CartItemId;
                    var newCartItemVM = query.Handle();
                    cartItems.Append(newCartItemVM);
                }
                    
            }
            double total = 0;
            cartItems.ForEach(x => total += x.Total);

            return new GetCartViewModel { UserId = UserId, CartItems = cartItems, CartTotal=total };
        }
    }
    public class GetCartViewModel
    {
        public int UserId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}
