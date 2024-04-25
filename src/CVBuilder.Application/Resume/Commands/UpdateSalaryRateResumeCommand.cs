using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateSalaryRateResumeCommand : IRequest<ResumeCardResult>
{
    public int ResumeId { get; set; }
    public decimal SalaryRate { get; set; }
}