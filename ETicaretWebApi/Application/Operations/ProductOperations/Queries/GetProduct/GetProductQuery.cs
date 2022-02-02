using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProduct
{
    public class GetProductQuery : IQuery<ProductViewModel>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public int ProductId { get; set; }
        public GetProductQuery(IMapper mapper, ETicaretDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ProductViewModel Handle()
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
            if (product is null)
            {
                throw new InvalidOperationException("Product is not exist");
            }
            return _mapper.Map<ProductViewModel>(product);
        }
    }
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string ColorId { get; set; }
        public int BrandId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
