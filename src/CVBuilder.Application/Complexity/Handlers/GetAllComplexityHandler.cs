using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Complexity.Queries;
using CVBuilder.Application.Complexity.Result;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Complexity.Handlers;

public class GetAllComplexityHandler : IRequestHandler<GetAllComplexitiesQuery, List<ComplexityResult>>
{
    private readonly IRepository<ProposalBuildComplexity, int> _complexityRepository;
    private readonly IMapper _mapper;

    public GetAllComplexityHandler(IRepository<ProposalBuildComplexity, int> complexityRepository, IMapper mapper)
    {
        _complexityRepository = complexityRepository;
        _mapper = mapper;
    }

    public async Task<List<ComplexityResult>> Handle(GetAllComplexitiesQuery request,
        CancellationToken cancellationToken)
    {
        var complexities = await _complexityRepository.Table
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<ComplexityResult>>(complexities);
        return result;
    }
}