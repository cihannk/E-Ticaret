using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.CategoryOperations.Queries.GetCategories
{
    public class GetCategoriesQuery : IQuery<List<CategoriesViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;

        public GetCategoriesQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoriesViewModel> Handle()
        {
            var categories = _context.Categories.ToList();
            return _mapper.Map<List<CategoriesViewModel>>(categories);
        }
    }
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
