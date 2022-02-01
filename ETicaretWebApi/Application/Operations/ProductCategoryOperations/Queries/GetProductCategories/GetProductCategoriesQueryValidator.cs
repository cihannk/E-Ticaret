using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductCategoryOperations.Queries.GetProductCategories
{
    public class GetProductCategoriesQueryValidator: AbstractValidator<GetProductCategoriesQuery>
    {
        public GetProductCategoriesQueryValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).NotEmpty();
        }
    }
}
