using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.ProductCategoryOperations.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public CreateProductCategoryModel Model { get; set; }

        public CreateProductCategoryCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var productCategory = _context.ProductCategories.FirstOrDefault(x => x.ProductId == Model.ProductId && x.CategoryId == Model.CategoryId);
            if (productCategory is not null)
            {
                throw new Exception("You can't add same category to product");
            }
            var cat = _context.Categories.FirstOrDefault(x => x.Id == Model.CategoryId);
            if (cat is null)
            {
                throw new Exception("Category is not exist");
            }
            _context.ProductCategories.Add(_mapper.Map<ProductCategory>(Model));
            _context.SaveChanges();
        }
    }
    public class CreateProductCategoryModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
