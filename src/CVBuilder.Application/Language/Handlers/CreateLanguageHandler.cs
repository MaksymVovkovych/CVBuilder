using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers;

public class CreateLanguageHandler : IRequestHandler<CreateLanguageCommand, LanguageResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Language, int> _repository;

    public CreateLanguageHandler(IRepository<Models.Entities.Language, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LanguageResult> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.LanguageName)) request.LanguageName = "";

        var model = new Models.Entities.Language
        {
            Name = request.LanguageName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var language = await _repository.CreateAsync(model);

        return _mapper.Map<LanguageResult>(language);
    }
}