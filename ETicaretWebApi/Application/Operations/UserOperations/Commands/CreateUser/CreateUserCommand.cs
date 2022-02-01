using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCart;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

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

            var user = _mapper.Map<User>(Model);
            // user.password = Hash(password)
            user.SignedUpDate = DateTime.Now;
            user.AuthRoleId = 0;

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
