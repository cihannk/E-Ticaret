using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProduct;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryNameQuery : IQuery<List<ProductViewModel>>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public GetProductsByCategoryNameQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string CategoryName { get; set; }
        public string Brands { get; set; }
        public string Price { get; set; }

        public List<ProductViewModel> Handle()
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name == CategoryName.ToLower());
            // kat elde edildi
            if (category == null)
            {
                throw new InvalidOperationException("Category has not found");
            }

            var productCategories = _context.ProductCategories.Where(x => x.CategoryId == category.Id).ToList();
            // o kat id ile elde edilen ürün idleri geldi
            List<Product> products = new List<Product>();

            foreach (var productCategory in productCategories)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == productCategory.ProductId);
                products.Add(product);
            }
            if (Brands is not null)
            {
                string [] brandIds = Brands.Split(',');
                List<Product> filtered = new List<Product>();
                foreach (var brandId in brandIds)
                {
                    var filteredProductsPiece = products.Where(x => x.BrandId == Int32.Parse(brandId));
                    filtered.AddRange(filteredProductsPiece);
                }
                products = filtered;
                
            }
            if (Price is not null)
            {
                if (!Price.Contains('-'))
                    throw new InvalidDataException("Price range must be declared");
                string[] priceRange = Price.Split('-');
                products = products.Where(x => x.Price >= Int32.Parse(priceRange[0]) && x.Price <= Int32.Parse(priceRange[1])).ToList();
            }
            return _mapper.Map<List<ProductViewModel>>(products);
        }
        
    }
}
