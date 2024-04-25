using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers;

public class DeleteLanguageHandler : IRequestHandler<DeleteLanguageCommand, bool>
{
    private readonly IRepository<Models.Entities.Language, int> _languageRepository;
    private readonly IMapper _mapper;

    public DeleteLanguageHandler(IRepository<Models.Entities.Language, int> languageRepository, IMapper mapper)
    {
        _languageRepository = languageRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        await _languageRepository.DeleteAsync(request.Id);
        return true;
    }
}