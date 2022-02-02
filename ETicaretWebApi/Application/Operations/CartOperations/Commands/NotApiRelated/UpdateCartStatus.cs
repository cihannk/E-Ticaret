using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.NotApiRelated
{
    public class UpdateCartStatus
    {
        private readonly ETicaretDbContext _context;
        public int CartId { get; set; }
        public UpdateCartStatus(ETicaretDbContext context)
        {
            _context = context;
        }
        public void UpdateAmount()
        {
            var cartCartItems = _context.CartCartItems.Where(x => x.CartId == CartId).ToList();
            var cart = _context.Carts.FirstOrDefault(x => x.Id == CartId);
            double total = 0;
            foreach (var cartItem in cartCartItems)
            {
                var cartItemNew = _context.CartItems.FirstOrDefault(x => x.Id == cartItem.CartItemId);
                total += cartItemNew.Total;
            }
            cart.CartTotal = total;
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }
    }
}
