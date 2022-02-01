using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.BrandOperations.Commands.CreateBrand
{
    public class CreateBrandCommand : ICommand
    {
        private readonly ETicaretDbContext _context;
        public CreateBrandModel Model { get; set; }
        private readonly IMapper _mapper;
        public CreateBrandCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var brand = _context.Brands.FirstOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            if (brand is not null)
            {
                throw new InvalidOperationException("Brand is exist");
            }
            brand = _mapper.Map<Brand>(Model);

            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

    }
    public class CreateBrandModel
    {
        public string Name { get; set; }
    }
}
    
