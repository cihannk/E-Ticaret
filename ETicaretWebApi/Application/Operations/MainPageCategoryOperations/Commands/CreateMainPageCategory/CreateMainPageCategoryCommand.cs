using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Commands.CreateMainPageCategory
{
    public class CreateMainPageCategoryCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public CreateMainPageCategoryModel Model { get; set; }
        public CreateMainPageCategoryCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var mainPageCategory = _context.MainPageCategories.FirstOrDefault(x => x.DisplayName.ToLower() == Model.DisplayName.ToLower());
            if (mainPageCategory != null)
            {
                throw new InvalidOperationException("MainPageCategory is already exist");
            }
            mainPageCategory = _mapper.Map<MainPageCategory>(Model);
            mainPageCategory.PathName = _context.Categories.FirstOrDefault(x => x.Id == Model.CategoryId).Name.ToLower();
           
            _context.MainPageCategories.Add(mainPageCategory);
            _context.SaveChanges();
        }
    }
    public class CreateMainPageCategoryModel
    {
        public int CategoryId { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
    }
}
