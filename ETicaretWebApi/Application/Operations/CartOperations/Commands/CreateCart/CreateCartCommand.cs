using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCart
{
    public class CreateCartCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        public int UserId { get; set; }
        public CreateCartCommand(ETicaretDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            // aynı kullanıcını 2 tane cart'ı olamaz
            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
            if (user == null)
                throw new InvalidOperationException("Cart cannot be created due to User not exist");
            var cart = _context.Carts.FirstOrDefault(x => x.UserId == UserId);
            if (cart is not null)
                throw new InvalidOperationException("There is another cart related to user");
            Cart newCart = new Cart();
            newCart.UserId = UserId;
            _context.Carts.Add(newCart);
            _context.SaveChanges();
        }
    }
}
