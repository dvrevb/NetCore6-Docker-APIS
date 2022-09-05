using Contacts.Services.Abstract;
using Contacts.Services.Concrete;
using DockerAPIS.Business.Abstract;
using DockerAPIS.Business.Concrete;
using DockerAPIS.Core.Settings;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.DataAccess.Concrete;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

IConfiguration configuration = builder.Configuration;

builder.Services.Configure<RedisSettings>(options =>
{
    options.ConnectionString = configuration.GetSection("Redis:ConnectionString").Value;
});
builder.Services.AddScoped<IContactDataAccess, ContactDataAccess>();
builder.Services.AddScoped<IContactService, ContactManager>();

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
