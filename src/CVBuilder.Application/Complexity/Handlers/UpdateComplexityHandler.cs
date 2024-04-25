using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Application.Complexity.Result;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Complexity.Handlers;

public class UpdateComplexityHandler : IRequestHandler<UpdateComplexityCommand, ComplexityResult>
{
    private readonly IRepository<ProposalBuildComplexity, int> _complexityRepository;
    private readonly IMapper _mapper;

    public UpdateComplexityHandler(IRepository<ProposalBuildComplexity, int> complexityRepository, IMapper mapper)
    {
        _complexityRepository = complexityRepository;
        _mapper = mapper;
    }

    public async Task<ComplexityResult> Handle(UpdateComplexityCommand request, CancellationToken cancellationToken)
    {
        var complexity = _mapper.Map<ProposalBuildComplexity>(request);
        complexity = await _complexityRepository.UpdateAsync(complexity);
        var result = _mapper.Map<ComplexityResult>(complexity);
        return result;
    }
}