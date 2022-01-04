using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.Title).MinimumLength(5).NotEmpty();
            RuleFor(x => x.Model.Price).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.ColorId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.ImageUrl).Must(x => x.StartsWith("http")).NotEmpty();
        }
    }
}
