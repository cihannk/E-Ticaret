using AutoMapper;
using ETicaretWebApi.Application.Operations.ProductCategoryOperations.Commands.CreateProductCategory;
using ETicaretWebApi.Application.Operations.ProductCategoryOperations.Queries.GetProductCategories;
using ETicaretWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("ProductCategories")]
    public class ProductCategoryController : Controller
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public ProductCategoryController(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("{productId}")]
        public IActionResult GetProductCategoriesByProductId(int productId)
        {
            GetProductCategoriesQuery command = new GetProductCategoriesQuery(_context, _mapper);
            command.ProductId = productId;

            GetProductCategoriesQueryValidator validator = new GetProductCategoriesQueryValidator();
            validator.ValidateAndThrow(command);

            return Ok(command.Handle());

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProductCategory([FromBody] CreateProductCategoryModel model)
        {
            CreateProductCategoryCommand command = new CreateProductCategoryCommand(_context, _mapper);
            command.Model = model;

            CreateProductCategoryCommandValidator validator = new CreateProductCategoryCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
