using AutoMapper;
using ETicaretWebApi.Application.Operations.UserOperations.Queries.CreateUser;
using ETicaretWebApi.Application.Operations.UserOperations.Queries.GetUser;
using ETicaretWebApi.Application.Operations.UserOperations.Queries.LoginUser;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Services.JWT;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;
        private readonly ITokenOperations _tokenOperations;

        public UserController(IMapper mapper, ETicaretDbContext context, IConfiguration configuration, ITokenOperations tokenOperations)
        {
            _mapper = mapper;
            _context = context;
            _tokenOperations = tokenOperations;
        }

        [HttpPost("Register")]
        public IActionResult CreateUser([FromBody] CreateUserModel model)
        {
            CreateUserQuery query = new CreateUserQuery(_context, _mapper, _tokenOperations);
            query.Model = model;

            CreateUserQueryValidator validator = new CreateUserQueryValidator();
            validator.ValidateAndThrow(query);

            string token = query.Handle();

            return Ok(new { token = token });
        }
        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody] LoginUserModel model)
        {
            LoginUserQuery query = new LoginUserQuery(_context, _mapper, _tokenOperations);
            query.Model = model;

            LoginUserQueryValidator validator = new LoginUserQueryValidator();
            validator.ValidateAndThrow(query);
            string token = query.Handle();

            return Ok(new { token = token});
        }
        [Authorize]
        [HttpGet("Profile/{userId}")]
        public IActionResult GetProfile(int userId)
        {
            GetUserQuery query = new GetUserQuery(_context, _mapper);
            query.UserId = userId;
            return Ok(query.Handle());
        }
    }
}
