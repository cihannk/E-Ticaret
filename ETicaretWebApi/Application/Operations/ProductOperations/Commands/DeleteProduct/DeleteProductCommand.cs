using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Commands.DeleteProduct
{
    public class DeleteProductCommand: ICommand
    {
        private readonly ETicaretDbContext _context;
        public int ProductId { get; set; }
        public DeleteProductCommand(ETicaretDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
            if (product is null)
            {
                throw new InvalidOperationException("Product is not exist");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
