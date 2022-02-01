using FluentValidation;

namespace ETicaretWebApi.Application.Operations.ProductOperations.Queries.GetProduct
{
    public class GetProductQueryValidator: AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).NotEmpty();
        }
    }
}
