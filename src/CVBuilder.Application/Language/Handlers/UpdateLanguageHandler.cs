using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers;

public class UpdateLanguageHandler : IRequestHandler<UpdateLanguageCommand, LanguageResult>
{
    private readonly IRepository<Models.Entities.Language, int> _languageRepository;
    private readonly IMapper _mapper;

    public UpdateLanguageHandler(IRepository<Models.Entities.Language, int> languageRepository, IMapper mapper)
    {
        _languageRepository = languageRepository;
        _mapper = mapper;
    }

    public async Task<LanguageResult> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var model = new Models.Entities.Language
        {
            Id = request.LanguageId,
            Name = request.LanguageName,
            UpdatedAt = DateTime.Now
        };
        var skill = await _languageRepository.UpdateAsync(model);
        return _mapper.Map<LanguageResult>(skill);
    }
}