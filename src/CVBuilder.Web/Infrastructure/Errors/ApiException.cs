using System.Collections.Generic;

namespace CVBuilder.Web.Infrastructure.Errors;

public class ApiException
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}