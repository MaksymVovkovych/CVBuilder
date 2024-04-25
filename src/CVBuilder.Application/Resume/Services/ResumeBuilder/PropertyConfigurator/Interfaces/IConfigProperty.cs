namespace CVBuilder.Application.Resume.Services.ResumeBuilder.PropertyConfigurator.Interfaces;

public interface IConfigProperty
{
    public string PropertyName { get; }
    public string ConfigureValue(object property);
}