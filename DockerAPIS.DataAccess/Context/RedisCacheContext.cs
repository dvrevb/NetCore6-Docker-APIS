using DockerAPIS.Core.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.DataAccess.Context
{
   
    public class RedisCacheContext
    {
        private Lazy<ConnectionMultiplexer>? lazyConnection;
        public RedisCacheContext(IOptions<RedisSettings> settings)
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(settings.Value.ConnectionString);
            });
        }
        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

    }
}
