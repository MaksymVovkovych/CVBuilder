using CVBuilder.Application.Identity.Commands;
using FluentValidation;

namespace CVBuilder.Application.Identity.Validators;

public class RefreshTokenValidation : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidation()
    {
        // RuleFor(x => x.Token)
        //     .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}