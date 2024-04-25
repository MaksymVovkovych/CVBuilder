using CVBuilder.Application.Resume.Commands;
using FluentValidation;

namespace CVBuilder.Application.Resume.Validators;

public class UpdateCvValidation : AbstractValidator<UpdateResumeCommand>
{
    // public UpdateCvValidation()
    // {
    //     RuleForEach(x => x.Skills).SetValidator(new CreateCvValidation.ValidationSkill());
    //     RuleForEach(x => x.UserLanguages).SetValidator(new CreateCvValidation.ValidationLanguage());
    // }
}