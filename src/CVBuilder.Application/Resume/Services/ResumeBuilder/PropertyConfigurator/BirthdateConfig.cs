using System;
using CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator.Interfaces;

namespace CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator;

public class BirthdateConfig : IConfigProperty
{
    public string PropertyName => "birthdate";

    public string ConfigureValue(object property)
    {
        var age = 0;
        if (property is DateTime birthdate)
        {
            var today = DateTime.Today;
            age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
        }

        return $"{age} years";
    }
}