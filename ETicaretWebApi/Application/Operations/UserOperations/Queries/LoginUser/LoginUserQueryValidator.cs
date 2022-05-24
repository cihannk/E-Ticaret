using FluentValidation;

namespace ETicaretWebApi.Application.Operations.UserOperations.Queries.LoginUser
{
    public class LoginUserQueryValidator: AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.Model.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Model.Password).MinimumLength(8).MaximumLength(100).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$").NotEmpty();
        }
    }
}
