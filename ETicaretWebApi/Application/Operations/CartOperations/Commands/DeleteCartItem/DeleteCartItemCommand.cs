using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.DeleteCartItem
{
    public class DeleteCartItemCommand: ICommand
    {
        private readonly ETicaretDbContext _context;
        public int Id { get; set; }

        public DeleteCartItemCommand(ETicaretDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == Id);
            if (cartItem is null)
            {
                throw new InvalidOperationException("CartItem cannot be deleted due to it's not exist");
            }
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
    }
}
