using DockerAPIS.Architecture.Web.Core;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
CommonStartup.CommonServiceConfiguration(new ServiceConfigurationOptions(builder.Services, builder.Configuration)
{
    UseSwagger = true
});
builder.Configuration.AddJsonFile($"Configuration/Ocelot/Ocelot.json", false, true);
builder.Configuration.AddJsonFile($"Configuration/Cache/Redis/RedisSettings.json", false, true);
builder.Services.AddOcelot();
var app = builder.Build();

CommonStartup.CommonAppConfiguration(new AppConfigurationOptions(app, builder.Environment));

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/ClassroomDocs/swagger/v1/swagger.json", "Classroom");
    options.SwaggerEndpoint("/ContactsDocs/swagger/v1/swagger.json", "Contacts");
});

app.UseOcelot().Wait();

app.Run();
