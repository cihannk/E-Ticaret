using AutoMapper;
using ETicaretWebApi.Application.ProductOperations.Commands.CreateProduct;
using ETicaretWebApi.Entitites;

namespace ETicaretWebApi.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // for product
            CreateMap<CreateProductModel, Product>();
        }
    }
}
