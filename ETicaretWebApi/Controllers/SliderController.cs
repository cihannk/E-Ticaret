using AutoMapper;
using ETicaretWebApi.Application.Operations.SliderOperations.Commands.CreateSlider;
using ETicaretWebApi.Application.Operations.SliderOperations.Queries.GetSliders;
using ETicaretWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class SliderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;
        public SliderController(IMapper mapper, ETicaretDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetSliders()
        {
            GetSlidersQuery query = new GetSlidersQuery(_context, _mapper);

            return Ok(query.Handle());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateSlider([FromBody] CreateSliderModel model)
        {
            CreateSliderCommand command = new CreateSliderCommand(_context, _mapper);
            command.Model = model;

            CreateSliderCommandValidator validator = new CreateSliderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok("Slider has successfully created");
        }
    }
}
