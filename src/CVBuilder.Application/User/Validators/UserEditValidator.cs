using CVBuilder.Application.User.Commands;
using FluentValidation;

namespace CVBuilder.Application.User.Validators;

public class UserEditValidator : AbstractValidator<UpdateUserCommand>
{
    public UserEditValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .Length(2, 25);
        RuleFor(c => c.LastName)
            .NotEmpty()
            .Length(2, 25);
    }
}