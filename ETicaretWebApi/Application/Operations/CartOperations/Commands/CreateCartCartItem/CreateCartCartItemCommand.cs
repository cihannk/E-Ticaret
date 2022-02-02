using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartCartItem
{
    public class CreateCartCartItemCommand: ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public CartCartItemModel Model { get; set; }

        public CreateCartCartItemCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var cart = _context.Carts.FirstOrDefault(x => x.Id == Model.CartId);
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == Model.CartItemId);
            if (cart == null)
                throw new InvalidOperationException("Cart is not exist");
            if (cartItem == null)
                throw new InvalidOperationException("CartItem is not exist");
            _context.CartCartItems.Add(_mapper.Map<CartCartItem>(Model));
            _context.SaveChanges();
        }
    }
    public class CartCartItemModel
    {
        public int CartId { get; set; }
        public int CartItemId { get; set; }
    }
}
