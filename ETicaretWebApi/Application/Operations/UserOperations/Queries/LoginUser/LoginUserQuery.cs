using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;
using ETicaretWebApi.Services.GeneratePassword;
using ETicaretWebApi.Services.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETicaretWebApi.Application.Operations.UserOperations.Queries.LoginUser
{
    public class LoginUserQuery : IQuery<string>
    {
        private readonly ETicaretDbContext _context;
        private readonly ITokenOperations _tokenOperations;
        private readonly IMapper _mapper;
        public LoginUserModel Model { get; set; }
        public LoginUserQuery(ETicaretDbContext context, IMapper mapper, ITokenOperations tokenOperations)
        {
            _mapper = mapper;
            _context = context;
            _tokenOperations = tokenOperations;
        }
       
        public string Handle()
        {
            var user = _context.Users.Include(x => x.AuthRole).FirstOrDefault(x => x.Email == Model.Email);
            if (user == null)
            {
                throw new InvalidOperationException("User not exist");
            }
            bool login = HashingOperations.VerifyPasswordHash(Model.Password, user.Password, user.Salt);
            if (login)
            {
                var token = _tokenOperations.GetToken(user);
                return token;
            }
            throw new InvalidOperationException("Email or password is wrong");
        }
    }
    public class LoginUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    //public class LoginUserViewModel
    //{
    //    public int Id { get; set; }
    //    public string Email { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Token { get; set; }
    //}
}
    
