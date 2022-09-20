using DockerAPIS.Architecture.Web.Core;
using DockerAPIS.Services.Contact.Manager.Business.Interface;
using DockerAPIS.Services.Contact.Manager.Mapper;
using DockerAPIS.Services.Contact.Manager.Business.Implementation;
using DockerAPIS.Services.Contact.Manager.Operation.Interfaces;
using DockerAPIS.Services.Contact.Manager.Operation.Implementation;
using DockerAPIS.Architecture.Data.Cache.Base;
using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Services.Contact.Model.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile($"Configuration/Cache/Redis/RedisSettings.json", false, true);
CommonStartup.CommonServiceConfiguration(new ServiceConfigurationOptions(builder.Services, builder.Configuration)
{
    AutoMapperProfile = new ContactMappingProfile(),
    UseSwagger = true
}, true) ;

builder.Services.AddScoped<IContactBusinessManager, ContactBusinessManager>();
builder.Services.AddScoped<IContactOperation, ContactOperation>();
builder.Services.AddScoped<ICache<Person>, RedisCaching<Person>>();
var app = builder.Build();

CommonStartup.CommonAppConfiguration(new AppConfigurationOptions(app, app.Environment)
{
    SwaggerSettings = new()
    {
        APIName = "contactsapi",
        SwaggerEndpoint = "/swagger/v1/swagger.json",
        Server = "/Contacts"
    }
});

app.Run();
