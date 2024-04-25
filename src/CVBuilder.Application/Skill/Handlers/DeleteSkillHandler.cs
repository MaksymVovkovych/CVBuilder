using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Skill.Handlers;
using Models.Entities;
public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, bool>
{
    private readonly IRepository<Skill, int> _skillRepository;

    public DeleteSkillHandler(IRepository<Skill, int> skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        await _skillRepository.SoftDeleteAsync(request.SkillId);
        return true;
    }
}