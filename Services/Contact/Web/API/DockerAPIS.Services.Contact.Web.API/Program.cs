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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile($"Configuration/Cache/Redis/RedisSettings.json", false, true);
CommonStartup.CommonServiceConfiguration(new ServiceConfigurationOptions(builder.Services, builder.Configuration)
{
    AutoMapperProfile = new ContactMappingProfile()
});

builder.Services.AddScoped<IContactBusinessManager, ContactBusinessManager>();
builder.Services.AddScoped<IContactOperation, ContactOperation>();
builder.Services.AddScoped<ICache<Person>, RedisCaching<Person>>();
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
