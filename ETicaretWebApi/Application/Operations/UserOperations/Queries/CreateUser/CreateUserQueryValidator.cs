using FluentValidation;

namespace ETicaretWebApi.Application.Operations.UserOperations.Queries.CreateUser
{
    public class CreateUserQueryValidator: AbstractValidator<CreateUserQuery>
    {
        public CreateUserQueryValidator()
        {
            RuleFor(x => x.Model.FirstName).MinimumLength(3).MaximumLength(12).NotEmpty();
            RuleFor(x => x.Model.LastName).MinimumLength(3).MaximumLength(12).NotEmpty();
            RuleFor(x => x.Model.Email).MinimumLength(8).MaximumLength(36).EmailAddress().NotEmpty();
            RuleFor(x => x.Model.Password).MinimumLength(8).MaximumLength(100).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$").NotEmpty();
        }
    }
}
