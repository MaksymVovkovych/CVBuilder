using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Application.Core.Infrastructure;

public class CVBuilderEngine : IEngine
{
    public virtual IServiceProvider ServiceProvider { get; protected set; }

    public virtual void RegisterDependencies(IServiceCollection services, AppSettings appSettings)
    {
        var typeFinder = new WebAppTypeFinder();

        //register engine
        services.AddSingleton<IEngine>(this);

        //register type finder
        services.AddSingleton<ITypeFinder>(typeFinder);

        //find dependency registrars provided by other assemblies
        var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

        //create and sort instances of dependency registrars
        var instances = dependencyRegistrars
            .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
            .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

        //register all provided dependencies
        foreach (var dependencyRegistrar in instances)
            dependencyRegistrar.Register(services, typeFinder, appSettings);

        services.AddSingleton(services);
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //find startup configurations provided by other assemblies
        var typeFinder = new WebAppTypeFinder();
        var startupConfigurations = typeFinder.FindClassesOfType<ICVBuilderStartup>();

        //create and sort instances of startup configurations
        var instances = startupConfigurations
            .Select(startup => (ICVBuilderStartup)Activator.CreateInstance(startup))
            .OrderBy(startup => startup.Order);

        //configure services
        foreach (var instance in instances)
            instance.ConfigureServices(services, configuration);

        //register mapper configurations
        //AddAutoMapper(services, typeFinder);

        //run startup tasks
        RunStartupTasks(typeFinder);

        //resolve assemblies here. otherwise, plugins can throw an exception when rendering views
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    public void ConfigureRequestPipeline(IApplicationBuilder application)
    {
        ServiceProvider = application.ApplicationServices;

        //find startup configurations provided by other assemblies
        var typeFinder = Resolve<ITypeFinder>();
        var startupConfigurations = typeFinder.FindClassesOfType<ICVBuilderStartup>();

        //create and sort instances of startup configurations
        var instances = startupConfigurations
            .Select(startup => (ICVBuilderStartup)Activator.CreateInstance(startup))
            .OrderBy(startup => startup.Order);

        //configure request pipeline
        foreach (var instance in instances)
            instance.Configure(application);
    }

    public T Resolve<T>(IServiceScope scope = null) where T : class
    {
        return (T)Resolve(typeof(T), scope);
    }

    public object Resolve(Type type, IServiceScope scope = null)
    {
        return GetServiceProvider(scope)?.GetService(type);
    }

    public virtual IEnumerable<T> ResolveAll<T>()
    {
        return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
    }

    public virtual object ResolveUnregistered(Type type)
    {
        Exception innerException = null;
        foreach (var constructor in type.GetConstructors())
            try
            {
                //try to resolve constructor parameters
                var parameters = constructor.GetParameters().Select(parameter =>
                {
                    var service = Resolve(parameter.ParameterType);
                    if (service == null)
                        throw new Exception("Unknown dependency");
                    return service;
                });

                //all is ok, so create instance
                return Activator.CreateInstance(type, parameters.ToArray());
            }
            catch (Exception ex)
            {
                innerException = ex;
            }

        throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);
    }

    protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
    {
        if (scope == null)
        {
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        return scope.ServiceProvider;
    }

    protected virtual void RunStartupTasks(ITypeFinder typeFinder)
    {
        //find startup tasks provided by other assemblies
        var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

        //create and sort instances of startup tasks
        //we startup this interface even for not installed plugins. 
        //otherwise, DbContext initializers won't run and a plugin installation won't work
        var instances = startupTasks
            .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
            .OrderBy(startupTask => startupTask.Order);

        //execute tasks
        foreach (var task in instances)
            task.ExecuteAsync().Wait();
    }

    private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        //check for assembly already loaded
        var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
        if (assembly != null)
            return assembly;

        //get assembly from TypeFinder
        var tf = Resolve<ITypeFinder>();
        assembly = tf?.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
        return assembly;
    }
}