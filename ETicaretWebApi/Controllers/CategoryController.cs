using AutoMapper;
using ETicaretWebApi.Application.Operations.CategoryOperations.Commands.CreateCategory;
using ETicaretWebApi.Application.Operations.CategoryOperations.Commands.DeleteCategory;
using ETicaretWebApi.Application.Operations.CategoryOperations.Queries.GetCategories;
using ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Commands.CreateMainPageCategory;
using ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Queries.GetMainPageCategories;
using ETicaretWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("Categories")]
    public class CategoryController: Controller
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            GetCategoriesQuery query = new GetCategoriesQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryModel model) {
            CreateCategoryCommand command = new CreateCategoryCommand(_context, _mapper);
            command.Model = model;

            CreateCategoryCommandValidator validator = new CreateCategoryCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            DeleteCategoryCommand command = new DeleteCategoryCommand(_context);
            command.CategoryId = id;

            command.Handle();
            return Ok();
        }
        // main page categories
        [Route("mainpage")]
        [HttpGet()]
        public IActionResult GetMainPageCategories ()
        {
            GetMainPageCategoriesQuery query = new GetMainPageCategoriesQuery(_context, _mapper);
            return Ok(query.Handle());
        }
        [Route("mainpage")]
        [HttpPost]
        public IActionResult CreateMainPageCategory([FromBody] CreateMainPageCategoryModel model)
        {
            CreateMainPageCategoryCommand command = new CreateMainPageCategoryCommand(_context, _mapper);
            command.Model = model;

            CreateMainPageCategoryCommandValidator validator = new CreateMainPageCategoryCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("MainPageCategory has successfully created");
        }

    }
}
