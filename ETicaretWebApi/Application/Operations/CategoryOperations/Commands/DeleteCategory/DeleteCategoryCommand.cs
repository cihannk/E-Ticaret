using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.CategoryOperations.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        public int CategoryId { get; set; }

        public DeleteCategoryCommand(ETicaretDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var cat = _context.Categories.FirstOrDefault(x => x.Id == CategoryId);
            if (cat == null)
            {
                throw new InvalidOperationException("Category is not exist");
            }
            _context.Categories.Remove(cat);
            _context.SaveChanges();
        }
    }
}
