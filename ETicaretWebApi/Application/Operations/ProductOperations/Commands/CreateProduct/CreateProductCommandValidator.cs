using ETicaretWebApi.Application.ProductOperations.Commands.CreateProduct;
using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Commands.CreateProduct
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Model.Title).MinimumLength(5).NotEmpty();
            RuleFor(x => x.Model.Price).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.ColorId).GreaterThan(0);
            RuleFor(x => x.Model.ImageUrl).Must(x => x.StartsWith("http"));
        }
    }
}
