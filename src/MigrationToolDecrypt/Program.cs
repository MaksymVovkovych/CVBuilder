using CVBuilder.EFContext;
using CVBuilder.Models.Entities;
using CVBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.OpenApi.Models;
using MigrationToolDecrypt;
using MigrationToolDecrypt.DbContexts;
using OpenXmlPowerTools;

var builder = WebApplication.CreateBuilder(args);

var configurations = GetConfiguration();



//builder.Services.AddTransient<IEncryptionProvider>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    builder.Services.AddEfCoreEncrypt(configurations);
builder.Services.AddDbContextFactory<EncryptDbContext>(opts => opts.UseNpgsql(configurations["DefaultConnection"]));
builder.Services.AddDbContextFactory<DecryptDbContext>(opts => opts.UseNpgsql(configurations["DefaultConnection"]));
builder.Services.AddTransient<DecryptData>();


builder.Services.AddIdentity<User, Role>(options =>
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
    .AddEntityFrameworkStores<EncryptDbContext>()
    .AddEntityFrameworkStores<DecryptDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHostedService<BackgroundDecryptService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}