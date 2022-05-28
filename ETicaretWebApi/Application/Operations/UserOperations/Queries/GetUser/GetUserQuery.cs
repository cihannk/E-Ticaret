using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.UserOperations.Queries.GetUser
{
    public class GetUserQuery: IQuery<UserModel>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public int UserId { get; set; }

        public GetUserQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserModel Handle()
        {
            var userEntity = _context.Users.FirstOrDefault(x => x.Id == UserId);
            if (userEntity == null)
            {
                throw new InvalidOperationException("User is not exist");
            }
            return _mapper.Map<UserModel>(userEntity);
        }
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
