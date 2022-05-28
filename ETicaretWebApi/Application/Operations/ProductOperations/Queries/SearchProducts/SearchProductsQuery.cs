using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Queries.SearchProducts
{
    public class SearchProductsQuery: IQuery<List<Product>>
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;

        public string Name { get; set; }
        public SearchProductsQuery(ETicaretDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<Product> Handle()
        {
            var products = _context.Products.Where(x => x.Title.ToLower().Contains(Name.ToLower()) || x.Description.ToLower().Contains(Name.ToLower())).ToList();
            if (products == null)
            {
                throw new InvalidOperationException("No product exist");
            }
            return products;
        }
    }
}
