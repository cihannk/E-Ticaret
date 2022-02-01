using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Queries.GetMainPageCategories
{
    public class GetMainPageCategoriesQuery : IQuery<List<MainPageCategoryViewModel>>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;

        public GetMainPageCategoriesQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<MainPageCategoryViewModel> Handle()
        {
            var mainPageCategories = _context.MainPageCategories.ToList();
            if (!mainPageCategories.Any())
            {
                throw new InvalidOperationException("Main page category not exist");
            }
            return _mapper.Map<List<MainPageCategoryViewModel>>(mainPageCategories);
        }
    }
    public class MainPageCategoryViewModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string PathName { get; set; }
        public string ImageUrl { get; set; }
    }
}
