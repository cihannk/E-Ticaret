using FluentValidation;

namespace ETicaretWebApi.Application.Operations.BrandOperations.Commands.CreateBrand
{
    public class CreateBrandCommandValidator: AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.Model.Name).MinimumLength(3).NotEmpty();
        }
    }
}
