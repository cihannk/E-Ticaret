using AutoMapper;
using ETicaretWebApi.Application.Abstract;
using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Application.Operations.SliderOperations.Commands.CreateSlider
{
    public class CreateSliderCommand : ICommand
    {
        public CreateSliderModel Model { get; set; }
        private readonly ETicaretDbContext _context;
        private readonly IMapper _mapper;

        public CreateSliderCommand(ETicaretDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var slider = _context.Sliders.FirstOrDefault(x => x.ProductId == Model.ProductId);
            var product = _context.Products.FirstOrDefault(x => x.Id == Model.ProductId);
            if (slider != null)
                throw new InvalidOperationException("You can't create slider have same product");
            if (product == null)
            {
                throw new InvalidOperationException("There is no Product related Slider");
            }
            var newSlider = _mapper.Map<Slider>(Model);
            newSlider.Price = product.Price;

            _context.Sliders.Add(newSlider);
            _context.SaveChanges();
        }   
    }
    public class CreateSliderModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
