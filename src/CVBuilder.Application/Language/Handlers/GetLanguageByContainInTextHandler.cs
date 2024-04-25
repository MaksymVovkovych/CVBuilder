using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Language.Handlers;

public class
    GetLanguageByContainInTextHandler : IRequestHandler<GetLanguageByContainInTextQuery, IEnumerable<LanguageResult>>
{
    private readonly IRepository<Models.Entities.Language, int> _languageRepository;
    private readonly IMapper _mapper;

    public GetLanguageByContainInTextHandler(IMapper mapper,
        IRepository<Models.Entities.Language, int> languageRepository)
    {
        _mapper = mapper;
        _languageRepository = languageRepository;
    }

    public async Task<IEnumerable<LanguageResult>> Handle(GetLanguageByContainInTextQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Content)) request.Content = "";

        var languages = await _languageRepository
            .Table
            .Where(language => language.Name.ToLower().StartsWith(request.Content))
            .Take(10)
            .ToListAsync(cancellationToken: cancellationToken);


        var result = _mapper.Map<IEnumerable<LanguageResult>>(languages);

        return result;
    }
}