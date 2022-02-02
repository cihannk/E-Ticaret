using AutoMapper;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartCartItem;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartItem;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.DeleteCartItem;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.UserAddsProductToCart;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.UserDeletesProductFromCart;
using ETicaretWebApi.Application.Operations.CartOperations.Queries.GetCart;
using ETicaretWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class CartController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;
        public CartController(IMapper mapper, ETicaretDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetCartByUserId(int id)
        {
            GetCartQuery command = new GetCartQuery(_mapper, _context);
            command.UserId = id;

            return Ok(command.Handle());
        }

        [HttpPost("cartItems")]
        public IActionResult CreateCartItem([FromBody] CreateCartItemModel model)
        {
            CreateCartItemCommand command = new CreateCartItemCommand(_context, _mapper);
            command.Model = model;

            CreateCartItemCommandValidator validator = new CreateCartItemCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok("CartItem successfully created");
        }
        [HttpPost("cartCartItems")]
        public IActionResult CreateCartCartItem([FromBody] CartCartItemModel model)
        {
            CreateCartCartItemCommand command = new CreateCartCartItemCommand(_context, _mapper);
            command.Model = model;

            //CreateCartItemCommandValidator validator = new CreateCartItemCommandValidator();
            //validator.ValidateAndThrow(command);

            command.Handle();

            return Ok("CartCartItem successfully created");
        }
        [HttpDelete("cartItems/{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            DeleteCartItemCommand command = new DeleteCartItemCommand(_context);
            command.Id = id;

            command.Handle();
            return Ok($"CartItem {id} has successfully deleted");
        }
        [HttpPost("user/addProduct")]
        public IActionResult UserAddProductToCart([FromBody] UserAddsProductToCartModel model)
        {
            UserAddsProductToCartCommand command = new UserAddsProductToCartCommand(_context, _mapper);
            command.Model = model;

            command.Handle();
            return Ok();
        }
        [HttpPost("user/change")]
        public IActionResult UserDeleteProductsFromCart([FromBody] UserChangesOrDeletesProductFromCartModel model)
        {
            UserChangesOrDeletesProductFromCartCommand command = new UserChangesOrDeletesProductFromCartCommand(_context, _mapper);
            command.Model = model;

            command.Handle();
            return Ok();
        }

    }
}
