using CVBuilder.Identity.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;

var configuration = GetConfiguration();


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);





var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}