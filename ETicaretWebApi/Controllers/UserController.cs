using AutoMapper;
using ETicaretWebApi.Application.Operations.UserOperations.Commands.CreateUser;
using ETicaretWebApi.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
        [ApiController]
        [Route("[Controller]s")]
        public class UserController : Controller
        {
            private readonly IMapper _mapper;
            private readonly ETicaretDbContext _context;
            public UserController(IMapper mapper, ETicaretDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            [HttpPost()]
            public IActionResult CreateUser([FromBody] CreateUserModel model)
            {
                CreateUserCommand command = new CreateUserCommand(_context, _mapper);
                command.Model = model;

                CreateUserCommandValidator validator = new CreateUserCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                return Ok("User has successfully created");
            }
    }
}
