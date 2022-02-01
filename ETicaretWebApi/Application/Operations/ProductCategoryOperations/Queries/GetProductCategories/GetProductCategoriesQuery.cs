using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductCategoryOperations.Queries.GetProductCategories
{
    public class GetProductCategoriesQuery: IQuery<List<Category>>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public int ProductId { get; set; }

        public GetProductCategoriesQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Category> Handle()
        {
            var productCategories = _context.ProductCategories.Where(x => x.ProductId == ProductId).ToList();
            List<Category> categories = new List<Category>();
            foreach (var category in productCategories)
            {
                var cat = _context.Categories.FirstOrDefault(x => x.Id == category.CategoryId);
                categories.Add(cat);
            }
            return categories;  
        }
    }
}
