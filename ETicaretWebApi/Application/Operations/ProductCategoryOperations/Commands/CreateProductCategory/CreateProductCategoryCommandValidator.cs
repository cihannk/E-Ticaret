using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductCategoryOperations.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandValidator: AbstractValidator<CreateProductCategoryCommand>
    {
        public CreateProductCategoryCommandValidator()
        {
            RuleFor(x => x.Model.CategoryId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.ProductId).GreaterThan(0).NotEmpty();
        }
    }
}
