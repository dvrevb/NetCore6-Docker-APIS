using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using DockerAPIS.Architecture.Concern.Options;
using AutoMapper;
using DockerAPIS.Architecture.AppException.Manager;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace DockerAPIS.Architecture.Web.Core
{
    public class CommonStartup
    {
        public static void CommonServiceConfiguration(ServiceConfigurationOptions options, bool hasCache = false)
        {
            if (hasCache)
                ConfigureRedis(options.Services, options.Config);

            options.Services.AddSingleton(typeof(ExceptionParser));

            if (options.UseSwagger)
                ConfigureSwagger(options);

            if (options.AutoMapperProfile != null)
                ConfigureAutoMapper(options.Services, options.AutoMapperProfile);
        }

        private static void ConfigureSwagger(ServiceConfigurationOptions options)
        {
            options.Services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Burak Çevik",
                        Email = "burak.cevik98@gmail.com"
                    }
                });
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => swaggerOptions.IncludeXmlComments(xmlFile));
                //swaggerOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
        }
        private static void ConfigureSwagger(AppConfigurationOptions options)
        {
            options.App.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, request) => swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = options.SwaggerSettings.Server } });
            });
            options.App.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(options.SwaggerSettings.SwaggerEndpoint, options.SwaggerSettings.APIName);
            });
        }
        private static void ConfigureAutoMapper(IServiceCollection services, Profile mappingProfile)
        {
            MapperConfiguration mapperConfig = new(config =>
            {
                config.AddProfile(mappingProfile);
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void ConfigureRedis(IServiceCollection services, IConfiguration config)
        {
            RedisSettings redisSettings = config.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
            services.AddSingleton(s => redis.GetDatabase());
            services.AddSingleton(redis);
        }
        public static void CommonAppConfiguration(AppConfigurationOptions options)
        {
            if (options.HostEnvironment.IsDevelopment())
            {
                options.App.UseDeveloperExceptionPage();
            }

            // options.App.UseExceptionHandler($"/{options.ErrorHandlerEndpoint}");

            options.App.UseRouting();

            //options.App.UseAuthentication();
            options.App.UseAuthorization();

            options.App.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (options.SwaggerSettings != null)
            {
                ConfigureSwagger(options);
            }
        }
    }
}
