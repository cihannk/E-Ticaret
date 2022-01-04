using AutoMapper;
using ETicaretWebApi.Application.Operations.ProductOperations.Commands.CreateProduct;
using ETicaretWebApi.Application.Operations.ProductOperations.Commands.DeleteProduct;
using ETicaretWebApi.Application.Operations.ProductOperations.Commands.UpdateProduct;
using ETicaretWebApi.Application.ProductOperations.Commands.CreateProduct;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class ProductController : Controller
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.ToList());
        }
        //[HttpGet("{id}")]
        //public IActionResult GetProductById(int id)
        //{
        //    GetProductQuery query = new GetProductQuery();
        //}

        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductModel product)
        {

           CreateProductCommand command = new CreateProductCommand(_context, _mapper);
           CreateProductCommandValidator validator = new CreateProductCommandValidator();

           command.Model = product;
           validator.ValidateAndThrow(command);

           command.Handle();
           return Ok("Product has been added");

        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromQuery] UpdateProductModel updateProductModel, int id)
        {
            UpdateProductCommand command = new UpdateProductCommand(_context);
            command.Model = updateProductModel;
            command.ProductId = id;

            UpdateProductCommandValidator validator = new UpdateProductCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("Product has successfully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            DeleteProductCommand command = new DeleteProductCommand(_context);
            command.ProductId = id;

            DeleteProductCommandValidator validator = new DeleteProductCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("Product has successfully deleted");

        }
    }
}
