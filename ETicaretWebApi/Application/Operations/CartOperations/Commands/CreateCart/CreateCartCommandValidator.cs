using FluentValidation;

namespace ETicaretWebApi.Application.Operations.CartOperations.Commands.CreateCart
{
    public class CreateCartCommandValidator: AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).NotEmpty();
        }
    }
}
