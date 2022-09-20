using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DockerAPIS.Architecture.Web.Core
{
    public record ServiceConfigurationOptions(IServiceCollection Services, IConfiguration Config)
    {
        public bool UseSwagger { get; set; }
        public Profile? AutoMapperProfile { get; set; }
    }
}
