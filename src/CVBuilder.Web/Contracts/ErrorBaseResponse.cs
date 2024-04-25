using System.Collections.Generic;

namespace CVBuilder.Web.Contracts;

public class ErrorBaseResponse
{
    public IEnumerable<string> Errors { get; set; }
}