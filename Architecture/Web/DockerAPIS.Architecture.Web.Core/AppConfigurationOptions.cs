using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Architecture.Web.Core
{
    public record AppConfigurationOptions(IApplicationBuilder App, IWebHostEnvironment HostEnvironment/*, string ErrorHandlerEndpoint*/)
    {
        public SwaggerSettings SwaggerSettings { get; set; }
    }

    public class SwaggerSettings
    {
        public string SwaggerEndpoint { get; set; }
        public string APIName { get; set; }
        public string Server { get; set; }
    }
}
