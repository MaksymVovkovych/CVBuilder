using System;
using CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator.Interfaces;

namespace CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator;

public class DateConfig : IConfigProperty
{
    public DateConfig(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; }

    public string ConfigureValue(object property)
    {
        if (property is DateTime birthdate)
        {
            var date = birthdate.ToString("MM/yyyy");
            return date;
        }

        throw new ArgumentException("Invalid type");
    }
}