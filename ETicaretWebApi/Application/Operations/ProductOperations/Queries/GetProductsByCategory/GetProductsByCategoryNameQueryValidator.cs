using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryNameQueryValidator: AbstractValidator<GetProductsByCategoryNameQuery>
    {
        public GetProductsByCategoryNameQueryValidator()
        {
            RuleFor(x => x.CategoryName).MinimumLength(3).NotEmpty();
        }
    }
}
