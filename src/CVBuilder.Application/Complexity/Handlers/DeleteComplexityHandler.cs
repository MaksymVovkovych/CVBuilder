using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Complexity.Handlers;

public class DeleteComplexityHandler : IRequestHandler<DeleteComplexityCommand, bool>
{
    private readonly IRepository<ProposalBuildComplexity, int> _complexityRepository;
    private readonly IMapper _mapper;

    public DeleteComplexityHandler(IRepository<ProposalBuildComplexity, int> complexityRepository, IMapper mapper)
    {
        _complexityRepository = complexityRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteComplexityCommand request, CancellationToken cancellationToken)
    {
        await _complexityRepository.DeleteAsync(request.Id);
        return true;
    }
}