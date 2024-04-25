using System.Collections.Generic;
using CVBuilder.Application.Language.Responses;
using MediatR;

namespace CVBuilder.Application.Language.Queries;

public class GetAllLanguagesQuery : IRequest<List<LanguageResult>>
{
}