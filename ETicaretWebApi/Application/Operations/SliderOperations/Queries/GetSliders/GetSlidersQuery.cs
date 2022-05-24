using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProduct;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.SliderOperations.Queries.GetSliders
{
    public class GetSlidersQuery: IQuery<List<SlidersViewModel>>
    {
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;
        public GetSlidersQuery(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<SlidersViewModel> Handle()
        {
            GetProductQuery getProductQuery = new GetProductQuery(_mapper, _context);
            var sliders = _context.Sliders.ToList();
            List<SlidersViewModel> newSliders = new List<SlidersViewModel>();
            foreach (var classicSlider in sliders)
            {
                getProductQuery.ProductId = classicSlider.ProductId;
                var product = getProductQuery.Handle();
                var newSlider = new SlidersViewModel { Description = classicSlider.Description, Id = classicSlider.Id, Price= classicSlider.Price, Product= product,Title= classicSlider.Title  };
                newSliders.Add(newSlider);
            }
            return newSliders;
        }
    }
    public class SlidersViewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
