using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using DockerAPIS.Architecture.Concern.Options;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace DockerAPIS.Architecture.Web.Core
{
    public class CommonStartup
    {
        public static void CommonServiceConfiguration(ServiceConfigurationOptions options)
        {
            ConfigureRedis(options.Services, options.Config);

            if (options.AutoMapperProfile != null)
            {
                ConfigureAutoMapper(options.Services, options.AutoMapperProfile);
            }
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
    }
}
