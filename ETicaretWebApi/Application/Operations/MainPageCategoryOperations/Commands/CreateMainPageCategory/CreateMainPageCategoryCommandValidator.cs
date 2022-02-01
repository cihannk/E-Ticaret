using FluentValidation;

namespace ETicaretWebApi.Application.Operations.MainPageCategoryOperations.Commands.CreateMainPageCategory
{
    public class CreateMainPageCategoryCommandValidator: AbstractValidator<CreateMainPageCategoryCommand>
    {
        public CreateMainPageCategoryCommandValidator()
        {
            RuleFor(x => x.Model.ImageUrl).MinimumLength(10).Must(x => x.StartsWith("http")).NotEmpty();
            RuleFor(x => x.Model.CategoryId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.DisplayName).MinimumLength(3).NotEmpty();
        }
    }
}
