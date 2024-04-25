using System.IO;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumePdfByUrlQuery : IRequest<Stream>
{
    public string Url { get; set; }
}