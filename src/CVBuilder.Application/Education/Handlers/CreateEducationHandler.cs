using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Education.Handlers;

internal class CreateEducationHandler : IRequestHandler<CreateEducationCommand, CreateEducationResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Education, int> _repository;

    public CreateEducationHandler(IRepository<Models.Entities.Education, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateEducationResult> Handle(CreateEducationCommand request,
        CancellationToken cancellationToken)
    {
        var newEducation = _mapper.Map<Models.Entities.Education>(request);
        await _repository.CreateAsync(newEducation);
        var result = _mapper.Map<CreateEducationResult>(newEducation);

        return result;
    }
}