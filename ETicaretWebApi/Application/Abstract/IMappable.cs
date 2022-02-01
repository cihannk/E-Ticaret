using AutoMapper;

namespace ETicaretWebApi.Application.Abstract
{
    public interface IMappable
    {
        IMapper _mapper { get; set; }
    }
}
