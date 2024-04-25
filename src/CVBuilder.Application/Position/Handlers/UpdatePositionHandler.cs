using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Position.Commands;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Position.Handlers;

public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand, PositionResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Position, int> _positionRepository;

    public UpdatePositionHandler(IRepository<Models.Entities.Position, int> positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionResult> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var model = new Models.Entities.Position
        {
            Id = request.PositionId,
            PositionName = request.PositionName,
            UpdatedAt = DateTime.Now
        };

        var result = await _positionRepository.UpdateAsync(model);
        return _mapper.Map<PositionResult>(result);
    }
}