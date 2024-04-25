using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Helpers;
using CVBuilder.Application.Identity.Services;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.EFContext;
using CVBuilder.Models.Entities;
using CVBuilder.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Add services to the application and configure service provider
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    /// <param name="webHostEnvironment">Hosting environment</param>
    /// <returns>Configured engine and app settings</returns>
    public static (IEngine, AppSettings) ConfigureApplicationServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        //let the operating system decide what TLS protocol version to use
        //see https://docs.microsoft.com/dotnet/framework/network-programming/tls
        ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

        //create default file provider
        CommonHelper.DefaultFileProvider = new CVBuilderFileProvider(webHostEnvironment);

        //add accessor to HttpContext
        services.AddHttpContextAccessor();

        services.AddTransient<IAppUserManager, AppUserManager>();

        var appSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSection);
        var appSettings = appSection.Get<AppSettings>();
        services.AddSingleton(appSettings);

        var jwtSettingsSection = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettingsSection);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        services.AddSingleton(jwtSettings);

        var swaggerSettingsSection = configuration.GetSection("SwaggerSettings");
        services.Configure<SwaggerSettings>(swaggerSettingsSection);
        var swaggerSettingsSettings = swaggerSettingsSection.Get<SwaggerSettings>();
        swaggerSettingsSettings.Description = "";
        services.AddSingleton(swaggerSettingsSettings);

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IShortUrlService, ShortUrlService>();

        //create engine and configure service provider
        var engine = EngineContext.Create();

        engine.ConfigureServices(services, configuration);

        return (engine, appSettings);
    }

    /// <summary>
    ///     Register HttpContextAccessor
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public static void AddHttpContextAccessor(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    /// <summary>
    ///     Adds services required for application session state
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public static void AddHttpSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.Name = "CVBuilder.Session";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        });
    }

    public static void AddCVBuilderDistributedCache(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "CVBuilder API", Version = "v1"});
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            //todo
            //options.DocumentFilter<SignalRSwaggerGen.SignalRSwaggerGen>(new List<Assembly> { typeof(DriverHub).Assembly });
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                //options.SignIn.RequireConfirmedAccount = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultPhoneProvider;
                //options.Stores.ProtectPersonalData = true;
                //options.Stores.MaxLengthForKeys = 128;
            })
            .AddErrorDescriber<IdentityErrorDescriber>()
            .AddEntityFrameworkStores<IdentityEfDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services, byte[] key)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            // LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
            ValidateLifetime = true,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = AccessTokenDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = AccessTokenDefaults.AuthenticationScheme;
                x.DefaultScheme = AccessTokenDefaults.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, AccessTokenAuthenticationHandler>(
                AccessTokenDefaults.AuthenticationScheme, _ => { });

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });
    }
} 