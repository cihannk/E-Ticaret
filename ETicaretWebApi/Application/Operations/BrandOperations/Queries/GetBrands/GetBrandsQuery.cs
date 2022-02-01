using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;

namespace ETicaretWebApi.Application.Operations.BrandOperations.Queries.GetBrands
{
    public class GetBrandsQuery : IQuery<List<BrandsViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ETicaretDbContext _context;

        public GetBrandsQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BrandsViewModel> Handle()
        {
            var brands = _context.Brands.ToList();
            return _mapper.Map<List<BrandsViewModel>>(brands);
        }
    }
    public class BrandsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
