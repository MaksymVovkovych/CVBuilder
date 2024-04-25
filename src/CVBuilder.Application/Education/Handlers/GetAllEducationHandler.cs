using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Education.Handlers;

internal class GetAllEducationHandler : IRequestHandler<GetAllEducationsCommand, List<EducationResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Education, int> _repository;

    public GetAllEducationHandler(IRepository<Models.Entities.Education, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<EducationResult>> Handle(GetAllEducationsCommand request,
        CancellationToken cancellationToken)
    {
        var educations = await _repository.GetListAsync();
        var result = _mapper.Map<List<EducationResult>>(educations);

        return result;
    }
}