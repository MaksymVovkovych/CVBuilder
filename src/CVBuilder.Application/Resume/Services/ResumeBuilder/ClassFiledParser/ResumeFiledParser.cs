using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Text;
using CVBuilder.Application.Resume.Responses.Docx;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser.Interfaces;
using CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator.Interfaces;

namespace CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser;

public class ResumeFiledParser : IClassFieldParser<ResumeDocx>
{
    private readonly IEnumerable<IConfigProperty> _configProperties;

    public ResumeFiledParser()
    {
        _configProperties = new List<IConfigProperty>
        {
            // new BirthdateConfig(),
            // new DateConfig("startDate"),
            // new DateConfig("endDate")
        };
    }

    public Dictionary<string, string> GetFieldsWithValues(ResumeDocx resourceClass, params string[] excludeProperties)
    {
        var fields = typeof(ResumeDocx).GetProperties();
        var dictionary = new Dictionary<string, string>();
        foreach (var field in fields)
        {
            var fieldType = field.PropertyType;

            if (fieldType.IsGenericType && typeof(List<>) == fieldType.GetGenericTypeDefinition())
                continue;

            if (excludeProperties != null && excludeProperties.Contains(field.Name.ToLowerInvariant()))
                continue;

            var propertyName = field.Name;
            propertyName = char.ToLowerInvariant(propertyName[0]) + propertyName[1..];

            var propertyValue = field.GetValue(resourceClass);

            var config = _configProperties.FirstOrDefault(x => x.PropertyName == propertyName);

            if (config != null) propertyValue = config.ConfigureValue(propertyValue);

            dictionary.Add(propertyName, propertyValue?.ToString());
        }

        return dictionary;
    }

    public List<ParsedListFields> GetListFieldsWithValues(ResumeDocx resourceClass)
    {
        var resultList = new List<ParsedListFields>();

        
        var fields = typeof(ResumeDocx).GetProperties();
        foreach (var field in fields)
        {
            var fieldType = field.PropertyType;
            if (fieldType.IsGenericType && typeof(List<>) == fieldType.GetGenericTypeDefinition())
            {
                var listName = ToCamelCase(field.Name);
                var list = field.GetValue(resourceClass) as IEnumerable;
                var values = new List<Dictionary<string, string>>();
                foreach (var val in list!)
                {
                    var nameAndValues = new Dictionary<string, string>();
                    foreach (var property in val.GetType().GetProperties())
                    {
                        var propertyName = ToCamelCase(property.Name);
                        var propertyValue = property.GetValue(val, null);

                        var config = _configProperties.FirstOrDefault(x => x.PropertyName == propertyName);

                        if (config != null)
                        {
                            propertyValue = config.ConfigureValue(propertyValue);
                            
                        }

                        nameAndValues.Add(propertyName, propertyValue?.ToString());
                    }

                    values.Add(nameAndValues);
                }

                var parsedList = new ParsedListFields
                {
                    ListName = listName,
                    ListValues = values
                };
                resultList.Add(parsedList);
            }
        }

        return resultList;
    }

    private static string ToCamelCase(string str)
    {
        return char.ToLowerInvariant(str[0]) + str[1..];
    }
}