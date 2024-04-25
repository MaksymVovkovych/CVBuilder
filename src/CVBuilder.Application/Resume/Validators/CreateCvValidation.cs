using CVBuilder.Application.Resume.Commands;
using FluentValidation;

namespace CVBuilder.Application.Resume.Validators;

public class CreateCvValidation : AbstractValidator<CreateResumeCommand>
{
    // public CreateCvValidation()
    // {
    //     RuleForEach(x => x.Skills).SetValidator(new ValidationSkill());
    //     RuleForEach(x => x.UserLanguages).SetValidator(new ValidationLanguage());
    // }
    //
    // public class ValidationSkill:AbstractValidator<UpdateSkillCommand>
    // {
    //     public ValidationSkill()
    //     {
    //         RuleFor(x => x.SkillName).NotNull().NotEmpty().When(x=>x.Id == null).WithMessage("Skill name must be not empty");
    //
    //     }
    // }
    //
    // public class ValidationLanguage:AbstractValidator<UpdateLanguageCommand>
    // {
    //     public ValidationLanguage()
    //     {
    //         RuleFor(x => x.LanguageName).NotNull().NotEmpty().When(x=>x.Id == null).WithMessage("Language name must be not empty");
    //
    //     }
    // }
}