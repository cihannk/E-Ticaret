using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator:AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).NotEmpty();
        }
    }
}
