using CVBuilder.Application.Language.Responses;
using MediatR;

namespace CVBuilder.Application.Language.Commands;

public class UpdateLanguageCommand : IRequest<LanguageResult>
{
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
}