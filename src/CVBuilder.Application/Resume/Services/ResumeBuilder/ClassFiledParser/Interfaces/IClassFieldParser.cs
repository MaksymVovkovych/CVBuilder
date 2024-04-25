using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser.Interfaces;

public interface IClassFieldParser<TClass>
{
    public Dictionary<string, string> GetFieldsWithValues(TClass resourceClass, params string[] excludeProperties);
    public List<ParsedListFields> GetListFieldsWithValues(TClass resourceClass);
}