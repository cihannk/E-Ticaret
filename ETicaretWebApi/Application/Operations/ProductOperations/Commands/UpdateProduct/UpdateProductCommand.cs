using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Commands.UpdateProduct
{
    public class UpdateProductCommand: ICommand
    {
        private readonly ETicaretDbContext _context;
        public UpdateProductModel Model { get; set; }
        private readonly IMapper _mapper;
        public int ProductId { get; set; }
        public UpdateProductCommand(ETicaretDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
            if (product is null)
            {
                throw new InvalidOperationException("Product is not exist to update");
            }
            if (_context.Products.Any(x => x.Title == Model.Title && x.Id != ProductId))
            {
                throw new InvalidOperationException("Updating product title is already exist in database");
            }
            product.Title = Model.Title == default ? product.Title : Model.Title;
            product.Price = Model.Price == default ? product.Price : Model.Price;
            product.ColorId = Model.ColorId == default ? product.ColorId : Model.ColorId;
            product.ImageUrl = Model.ImageUrl == default ? product.ImageUrl : Model.ImageUrl;

            _context.SaveChanges();
        }
    }
    public class UpdateProductModel
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public int ColorId { get; set; }
        public string ImageUrl { get; set; }
    }
}
