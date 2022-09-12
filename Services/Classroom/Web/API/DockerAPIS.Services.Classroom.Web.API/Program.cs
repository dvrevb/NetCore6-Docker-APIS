using DockerAPIS.Architecture.Web.Core;
using DockerAPIS.Services.Classroom.ExternalData.Manager.Interfaces;
using DockerAPIS.Services.Classroom.ExternalData.Manager.Service.Contacts;
using DockerAPIS.Services.Classroom.Manager.Business.Implementation;
using DockerAPIS.Services.Classroom.Manager.Business.Interface;
using DockerAPIS.Services.Classroom.Manager.Mapper;
using DockerAPIS.Services.Classroom.Manager.Operation.Implementation;
using DockerAPIS.Services.Classroom.Manager.Operation.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

CommonStartup.CommonServiceConfiguration(new ServiceConfigurationOptions(builder.Services, builder.Configuration)
{
    AutoMapperProfile = new ClassroomMappingProfile()
});

builder.Services.AddScoped<IClassroomBusinessManager, ClassroomBusinessManager>();
builder.Services.AddScoped<IClassroomCacheOperation, ClassroomCacheOperation>();
builder.Services.AddScoped<IContactDataStore, ContactsServiceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();