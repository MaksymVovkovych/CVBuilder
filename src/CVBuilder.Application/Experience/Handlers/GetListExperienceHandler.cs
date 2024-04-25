using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Experience.Queries;
using CVBuilder.Application.Experience.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Experience.Handlers;

public class GetListExperienceHandler : IRequestHandler<GetAllExperiencesQuery, List<ExperienceResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Experience, int> _repository;

    public GetListExperienceHandler(IRepository<Models.Entities.Experience, int> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ExperienceResult>> Handle(GetAllExperiencesQuery request,
        CancellationToken cancellationToken)
    {
        var experience = await _repository.Table.ToListAsync(cancellationToken);

        var result = _mapper.Map<List<ExperienceResult>>(experience);

        return result;
    }
}