using FluentValidation;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCartItem
{
    public class CreateCartItemCommandValidator: AbstractValidator<CreateCartItemCommand>
    {
        public CreateCartItemCommandValidator()
        {
            RuleFor(x => x.Model.Amount).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.ProductId).GreaterThan(0).NotEmpty();
        }
    }
}
