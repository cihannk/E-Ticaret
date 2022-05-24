using FluentValidation;

namespace ETicaretWebApi.Application.Operations.SliderOperations.Commands.CreateSlider
{
    public class CreateSliderCommandValidator: AbstractValidator<CreateSliderCommand>
    {
        public CreateSliderCommandValidator()
        {
            RuleFor(x => x.Model.ProductId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Model.Description).MinimumLength(16).NotEmpty();
            RuleFor(x => x.Model.Title).MinimumLength(12).NotEmpty();
        }
    }
}
