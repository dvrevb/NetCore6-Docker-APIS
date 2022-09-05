
using DockerAPIS.Core.Models;
using DockerAPIS.Core.Settings;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.DataAccess.Caching;
using DockerAPIS.DataAccess.Context;
using DockerAPIS.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.DataAccess.Concrete
{
    public class ContactDataAccess : RedisCachingBase<Contact>, IContactDataAccess
    {
        private readonly RedisCacheContext _redisCacheContext;

        public ContactDataAccess(IOptions<RedisSettings> settings) : base(settings)
        {
            _redisCacheContext = new RedisCacheContext(settings);
        }
    }
}
