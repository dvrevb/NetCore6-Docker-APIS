using DockerAPIS.Architecture.Data.Cache.Base;
using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Architecture.Web.Core;
using DockerAPIS.Services.Classroom.ExternalData.Manager.Interfaces;
using DockerAPIS.Services.Classroom.ExternalData.Manager.Service.Contacts;
using DockerAPIS.Services.Classroom.Manager.Business.Implementation;
using DockerAPIS.Services.Classroom.Manager.Business.Interface;
using DockerAPIS.Services.Classroom.Manager.Mapper;
using DockerAPIS.Services.Classroom.Manager.Operation.Implementation;
using DockerAPIS.Services.Classroom.Manager.Operation.Interface;
using DockerAPIS.Services.Classroom.Model.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Configuration.AddJsonFile($"Configuration/Cache/Redis/RedisSettings.json");
CommonStartup.CommonServiceConfiguration(new ServiceConfigurationOptions(builder.Services, builder.Configuration)
{
    AutoMapperProfile = new ClassroomMappingProfile(),
    UseSwagger = true
},true);;

builder.Services.AddScoped<IClassroomBusinessManager, ClassroomBusinessManager>();
builder.Services.AddScoped<IClassroomOperations, ClassroomOperations>();
builder.Services.AddScoped<IContactDataStore, ContactsServiceManager>();
builder.Services.AddScoped<ICache<Lecture>, RedisCaching<Lecture>>();
var app = builder.Build();

CommonStartup.CommonAppConfiguration(new AppConfigurationOptions(app, app.Environment)
{
    SwaggerSettings = new()
    {
        APIName = "classroomapi",
        SwaggerEndpoint = "/swagger/v1/swagger.json",
        Server = "/Classroom"
    }
});

app.Run();
