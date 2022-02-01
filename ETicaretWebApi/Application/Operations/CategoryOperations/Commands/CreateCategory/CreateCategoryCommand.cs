using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.CategoryOperations.Commands.CreateCategory
{
    public class CreateCategoryCommand: ICommand
    {
        private readonly ETicaretDbContext _context;
        public CreateCategoryModel Model { get; set; }
        private readonly IMapper _mapper;
        public CreateCategoryCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if (category is not null)
            {
                throw new InvalidOperationException("Category is exist");
            }
            category = _mapper.Map<Category>(Model);

            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
    public class CreateCategoryModel
    {
        public string Name { get; set; }
    }
}
