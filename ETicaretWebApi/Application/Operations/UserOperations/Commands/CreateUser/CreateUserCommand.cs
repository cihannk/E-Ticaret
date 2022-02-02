using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCart;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;
using ETicaretWebApi.Services.GeneratePassword;

namespace ETicaretWebApi.Application.Operations.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserModel Model { get; set; }

        public CreateUserCommand(ETicaretDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            // aynı kullanıcı adı veya maille user yaratılamaz
            var userToFind = _context.Users.FirstOrDefault(x => x.Email == Model.Email);
            if (userToFind is not null)
                throw new InvalidOperationException("User with same email already exist");

            var user = new User { Email= Model.Email, FirstName= Model.FirstName, LastName= Model.LastName, AuthRoleId=0, SignedUpDate= DateTime.Now };

            byte[] passwordSalt, passwordHash;
            HashingOperations.GenerateHash(Model.Password, out passwordSalt, out passwordHash);

            user.Password = passwordHash;
            user.Salt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            // create cart dedicated to user
            CreateCartCommand cartCommand = new CreateCartCommand(_context);
            User userToCart = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            cartCommand.UserId = userToCart.Id;
            cartCommand.Handle();
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
