using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Queries.GetCartItem
{
    
    public class GetCartItemQuery: IQuery<CartItemViewModel>
    {
        private readonly ETicaretDbContext _context;
        public int CartItemId { get; set; }
        public GetCartItemQuery(ETicaretDbContext context)
        {
            _context = context;
        }

        public CartItemViewModel Handle()
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == CartItemId);
            if (cartItem == null)
                throw new InvalidOperationException("There is no CartItem");

            var product = _context.Products.FirstOrDefault(x => x.Id == cartItem.ProductId);
            if (product == null)
                throw new InvalidOperationException("There is no Product related to CartItem");
            var result = new CartItemViewModel { Id = cartItem.Id, Amount= cartItem.Amount, Product = product, Total= cartItem.Total };

            return result;
        }
    }
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public double Total { get; set; }
    }
}
