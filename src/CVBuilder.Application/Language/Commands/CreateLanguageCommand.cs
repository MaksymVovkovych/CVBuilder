using CVBuilder.Application.Language.Responses;
using MediatR;

namespace CVBuilder.Application.Language.Commands;

public class CreateLanguageCommand : IRequest<LanguageResult>
{
    public string LanguageName { get; set; }
}