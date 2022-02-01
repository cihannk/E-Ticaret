using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartItem
{
    public class CreateCartItemCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public CreateCartItemModel Model { get; set; }

        public CreateCartItemCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == Model.ProductId);

            if (product == null)
                throw new InvalidOperationException("Product that you have trying to add cart is not exist");

            CartItem item = _mapper.Map<CartItem>(Model);
            item.Total = item.Amount * product.Price;

            _context.CartItems.Add(item);
            _context.SaveChanges();
        }
    }
    public class CreateCartItemModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
