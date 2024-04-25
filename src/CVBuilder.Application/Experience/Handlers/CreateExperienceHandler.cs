using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Experience.Commands;
using CVBuilder.Application.Experience.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Experience.Handlers;

internal class CreateExperienceHandler : IRequestHandler<CreateExperienceCommand, CreateExperienceResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Experience, int> _repository;

    public CreateExperienceHandler(IRepository<Models.Entities.Experience, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateExperienceResult> Handle(CreateExperienceCommand request,
        CancellationToken cancellationToken)
    {
        var experience = _mapper.Map<Models.Entities.Experience>(request);
        await _repository.CreateAsync(experience);
        var result = _mapper.Map<CreateExperienceResult>(experience);

        return result;
    }
}