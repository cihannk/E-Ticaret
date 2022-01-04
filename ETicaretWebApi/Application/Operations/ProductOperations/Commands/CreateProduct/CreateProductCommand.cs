using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.ProductOperations.Commands.CreateProduct
{
    public class CreateProductCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        public CreateProductModel Model { get; set; }
        private readonly IMapper _mapper;
        public CreateProductCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            Product product = _context.Products.FirstOrDefault(x => x.Title == Model.Title);
            if (product is not null)
            {
                throw new InvalidOperationException("Product is exist");
            }
            product = _mapper.Map<Product>(Model);
            product.ListingDate = DateTime.Now;

            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
    public class CreateProductModel
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int ColorId { get; set; }
        public string ImageUrl { get; set; }
    }
}
