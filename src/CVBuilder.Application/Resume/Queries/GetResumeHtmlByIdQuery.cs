using System.Collections.Generic;
using CVBuilder.Application.Proposal.Queries;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumeHtmlByIdQuery : IRequest<string>
{
    public int ResumeId { get; set; }
    public int? UserId { get; set; }
    public IEnumerable<string> UserRoles { get; set; }
    public PrintFooter PrintFooter { get; set; } = PrintFooter.NotPrint;
}