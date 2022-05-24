using AutoMapper;
using ETicaretWebApi.Application.Operations.BrandOperations.Commands.CreateBrand;
using ETicaretWebApi.Application.Operations.BrandOperations.Queries.GetBrands;
using ETicaretWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BrandController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;
        public BrandController(IMapper mapper, ETicaretDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetBrands()
        {
            GetBrandsQuery query = new GetBrandsQuery(_context, _mapper);

            return Ok(query.Handle());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateBrand([FromBody] CreateBrandModel model)
        {
            CreateBrandCommand command = new CreateBrandCommand(_context, _mapper);
            command.Model = model;

            CreateBrandCommandValidator validator = new CreateBrandCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("Brand has successfully created");
        }
    }
}
