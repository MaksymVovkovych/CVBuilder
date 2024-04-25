using System.Collections.Generic;
using CVBuilder.Application.Language.Responses;
using MediatR;

namespace CVBuilder.Application.Language.Queries;

public class GetLanguageByContainInTextQuery : IRequest<IEnumerable<LanguageResult>>
{
    public string Content { get; set; }
}