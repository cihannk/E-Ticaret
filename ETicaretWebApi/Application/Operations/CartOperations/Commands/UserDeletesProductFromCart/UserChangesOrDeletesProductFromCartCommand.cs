using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.NotApiRelated;
using ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProduct;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.UserDeletesProductFromCart
{
    public class UserChangesOrDeletesProductFromCartCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public UserChangesOrDeletesProductFromCartModel Model { get; set; }

        public UserChangesOrDeletesProductFromCartCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            UpdateCartStatus updateCartStatus = new UpdateCartStatus(_context);
            GetProductQuery getProductQuery = new GetProductQuery(_mapper, _context);
            var cartCartItems = _context.CartCartItems.Where(x => x.CartId == Model.UserCartId).ToList();
            foreach (var cartItem in cartCartItems)
            {
                // cartcartıtemlerden cartitemlere geçiş yapılıp userin sailmek istediği ürün bulunur
                var cartItemOne = _context.CartItems.FirstOrDefault(x => x.Id == cartItem.CartItemId);
                if (cartItemOne.ProductId == Model.ProductId)
                {
                    cartItemOne.Amount = Model.Amount;

                    getProductQuery.ProductId = cartItemOne.ProductId;
                    var product = getProductQuery.Handle();

                    cartItemOne.Total = product.Price * cartItemOne.Amount;

                    if (cartItemOne.Amount <= 0)
                    {
                        _context.CartItems.Remove(cartItemOne);
                        _context.CartCartItems.Remove(cartItem);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.CartItems.Update(cartItemOne);
                        _context.SaveChanges();
                    }
                    updateCartStatus.CartId = Model.UserCartId;
                    updateCartStatus.UpdateAmount();
                }
            }
        }
    }
    public class UserChangesOrDeletesProductFromCartModel
    {
        public int UserCartId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
