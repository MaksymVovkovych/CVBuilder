using CVBuilder.Application.Identity.Commands;
using FluentValidation;

namespace CVBuilder.Application.Identity.Validators;

public class WebLoginValidation : AbstractValidator<WebLoginCommand>
{
    public WebLoginValidation()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}