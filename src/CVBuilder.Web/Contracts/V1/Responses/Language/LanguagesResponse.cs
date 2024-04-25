using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Responses.Language;

public class LanguagesResponse
{
    public List<LanguageResponse> Languages { get; set; }
}