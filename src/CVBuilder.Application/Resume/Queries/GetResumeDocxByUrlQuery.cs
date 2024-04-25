using System.IO;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumeDocxByUrlQuery : IRequest<Stream>
{
    public string Url { get; set; }
    public GetResumeDocxByUrlQuery(string url)
    {
        Url = url;
    }
}