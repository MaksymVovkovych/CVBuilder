using CVBuilder.Application.Client.Commands;
using CVBuilder.Application.Core.Constants;
using FluentValidation;

namespace CVBuilder.Application.Client.Validators;

public class UpdateClientValidation : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientValidation()
    {
        RuleFor(c => c.FirstName)
            // .NotEmpty()
            ;
        RuleFor(c => c.LastName)
            // .NotEmpty()
            ;
        RuleFor(c => c.PhoneNumber)
            .Matches(RegexConstants.PHONE_ALL_REGEX);
        RuleFor(c => c.Site)
            .Matches(RegexConstants.SITE_REGEX);
    }
}