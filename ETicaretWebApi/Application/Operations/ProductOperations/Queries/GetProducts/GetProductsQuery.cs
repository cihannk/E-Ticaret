using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProducts
{
    public class GetProductsQuery : IQuery<List<ProductsViewModel>>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public GetProductsQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ProductsViewModel> Handle()
        {
            var products = _context.Products.ToList();
            return _mapper.Map<List<ProductsViewModel>>(products);
        }
    }
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ListingDate { get; set; }
    }
}
