using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.UpdateCartItem
{
    public class UpdateCardItemCommand: ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCartItemModel Model { get; set; }
        public int CartItemId { get; set; }

        public UpdateCardItemCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == CartItemId);
            var product = _context.Products.FirstOrDefault(x => x.Id == Model.ProductId);

            if (product == null)
                throw new InvalidOperationException("CartItem cannot update due to product not exist");
            if (cartItem == null)
                throw new InvalidOperationException("CartItem cannot update due to it's not exist");
            cartItem.Amount = Model.Amount;
            cartItem.Total = calculateNewTotal(product);
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }
        public double calculateNewTotal (Product product)
        {
            double total = Model.Amount * product.Price;
            return total;
        }
    }

    public class UpdateCartItemModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
