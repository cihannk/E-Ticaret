using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;
using ETicaretWebApi.Services.GeneratePassword;
using ETicaretWebApi.Services.JWT;
using Microsoft.EntityFrameworkCore;

namespace ETicaretWebApi.Application.Operations.UserOperations.Queries.CreateUser
{
    public class CreateUserQuery : IQuery<string>
    {
        private readonly ETicaretDbContext _context;
        private readonly ITokenOperations _tokenOperations;
        private readonly IMapper _mapper;
        public CreateUserModel Model { get; set; }

        public CreateUserQuery(ETicaretDbContext context, IMapper mapper, ITokenOperations tokenOperations)
        {
            _mapper = mapper;
            _context = context;
            _tokenOperations = tokenOperations;
        }

        public string Handle()
        {
            // aynı kullanıcı adı veya maille user yaratılamaz
            var userToFind = _context.Users.FirstOrDefault(x => x.Email == Model.Email);
            if (userToFind is not null)
                throw new InvalidOperationException("User with same email already exist");

            var user = _mapper.Map<User>(Model);

            byte[] passwordSalt, passwordHash;
            HashingOperations.GenerateHash(Model.Password, out passwordSalt, out passwordHash);

            user.Password = passwordHash;
            user.Salt = passwordSalt;
            user.AuthRoleId = 2;
            user.SignedUpDate = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();

            //// create cart dedicated to user
            //CreateCartCommand cartCommand = new CreateCartCommand(_context);
            //User userToCart = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            //cartCommand.UserId = userToCart.Id;
            //cartCommand.Handle();
            var userDb = _context.Users.Include(x => x.AuthRole).FirstOrDefault(x => x.Email == Model.Email);
            return _tokenOperations.GetToken(userDb);
        }
    }
    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
