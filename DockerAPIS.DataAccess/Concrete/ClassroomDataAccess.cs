
using DockerAPIS.Core.Models;
using DockerAPIS.Core.Settings;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.DataAccess.Caching;
using DockerAPIS.DataAccess.Context;
using DockerAPIS.Entities;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DockerAPIS.DataAccess.Concrete
{
    public class ClassroomDataAccess : RedisCachingBase<Lecture>, IClassroomDataAccess
    {
        private readonly RedisCacheContext _redisCacheContext;

        public ClassroomDataAccess(IOptions<RedisSettings> settings) : base(settings)
        {
            _redisCacheContext = new RedisCacheContext(settings);
        }
    }
}
