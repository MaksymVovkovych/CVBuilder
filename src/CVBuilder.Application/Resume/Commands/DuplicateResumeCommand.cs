using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses.Shared;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class DuplicateResumeCommand : IRequest<ResumeResult>
{
    public int ResumeId { get; set; }
    public int? UserId { get; set; }
    public List<string> UserRoles { get; set; }
}