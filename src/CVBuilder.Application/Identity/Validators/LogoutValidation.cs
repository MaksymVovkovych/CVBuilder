using CVBuilder.Application.Identity.Commands;
using FluentValidation;

namespace CVBuilder.Application.Identity.Validators;

public class LogoutValidation : AbstractValidator<LogoutCommand>
{
    public LogoutValidation()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}