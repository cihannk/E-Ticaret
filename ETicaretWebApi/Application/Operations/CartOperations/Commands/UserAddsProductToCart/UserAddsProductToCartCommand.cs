using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartCartItem;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartItem;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.NotApiRelated;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.UpdateCartItem;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.UserAddsProductToCart
{
    public class UserAddsProductToCartCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public UserAddsProductToCartModel Model { get; set; }

        public UserAddsProductToCartCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var userCart = _context.Carts.FirstOrDefault(x => x.Id == Model.UserCartId);
            UpdateCartStatus updateCartStatus = new UpdateCartStatus(_context);
            if (userCart is null)
                throw new InvalidOperationException("User cart is not exist");

            CreateCartItemCommand command = new CreateCartItemCommand(_context, _mapper);
            CreateCartCartItemCommand command2 = new CreateCartCartItemCommand(_context, _mapper);
            List<SpecialCartItemModel> specialCartItemModels = new List<SpecialCartItemModel>();
            // userin girdiği bütün cartitemler
            foreach (var cartItem in Model.CartItems)
            {
                var existCartItem = _context.CartItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                if (existCartItem is null)
                {
                    // create new CartItem
                    command.Model = cartItem;
                    command.Handle();
                    var cartItemN = _context.CartItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId && x.Amount == cartItem.Amount);
                    specialCartItemModels.Add(new SpecialCartItemModel { CartItem=cartItemN, Exist= false});
                }
                else
                {
                    specialCartItemModels.Add(new SpecialCartItemModel { Exist= true, CartItem= existCartItem, CartItemToUpdate=cartItem});
                }
                
            }

            foreach (var specialCartItem in specialCartItemModels)
            {
                if (specialCartItem.Exist)
                {
                    // change value of cartItem
                    UpdateCardItemCommand updateCardItemCommand = new UpdateCardItemCommand(_context, _mapper);
                    updateCardItemCommand.CartItemId = specialCartItem.CartItem.Id;

                    updateCardItemCommand.Model = new UpdateCartItemModel { Amount = specialCartItem.CartItem.Amount + specialCartItem.CartItemToUpdate.Amount, ProductId= specialCartItem.CartItemToUpdate.ProductId};
                    updateCardItemCommand.Handle();
                }
                else
                {
                    command2.Model = new CartCartItemModel { CartId = userCart.Id, CartItemId = specialCartItem.CartItem.Id };
                    command2.Handle();
                }
                
            }
            // Update total of cart 
            updateCartStatus.CartId = Model.UserCartId;
            updateCartStatus.UpdateAmount();
        }
    }
    public class UserAddsProductToCartModel
    {
        public CreateCartItemModel[] CartItems { get; set; }
        public int UserCartId { get; set; }

    }
    public class SpecialCartItemModel
    {
        public bool Exist { get; set; }
        public CartItem CartItem { get; set; }
        public CreateCartItemModel CartItemToUpdate { get; set; }
    }
}
