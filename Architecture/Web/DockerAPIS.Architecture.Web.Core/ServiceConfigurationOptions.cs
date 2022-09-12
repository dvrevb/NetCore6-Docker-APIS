using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DockerAPIS.Architecture.Web.Core
{
    public record ServiceConfigurationOptions(IServiceCollection Services, IConfiguration Config)
    {
        public Profile? AutoMapperProfile { get; set; }
    }
}
