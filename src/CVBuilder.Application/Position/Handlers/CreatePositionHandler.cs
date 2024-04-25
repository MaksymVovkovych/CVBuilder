using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Position.Commands;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Position.Handlers;

public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, PositionResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Position, int> _positionRepository;

    public CreatePositionHandler(IRepository<Models.Entities.Position, int> positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionResult> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PositionName)) request.PositionName = "";

        var model = new Models.Entities.Position
        {
            PositionName = request.PositionName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var result = await _positionRepository.CreateAsync(model);
        return _mapper.Map<PositionResult>(result);
    }
}