using DockerAPIS.Architecture.Data.Cache.Base;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DockerAPIS.Architecture.Data.Cache.Concrete.Redis
{
    public class RedisCaching<TEntity> : ICache<TEntity>
    {
        private readonly IDatabase redisDb;
        private readonly IConnectionMultiplexer multiplexer;
        public RedisCaching(IDatabase redisDb, IConnectionMultiplexer multiplexer)
        {
            this.redisDb = redisDb;
            this.multiplexer = multiplexer;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            /*get all data*/
            IServer server = multiplexer.GetServer(multiplexer.GetEndPoints().First());
            IEnumerable<string> keys = server.Keys().Select(key => (string)key).ToList();

            List<TEntity> values = new List<TEntity>();
            foreach (var item in keys)
            {
                var value = JsonConvert.DeserializeObject<TEntity>(await redisDb.StringGetAsync(item));
                if (value != null)
                    values.Add(value);
            }
            return values;
        }

        public async Task<TEntity> GetByKey(string key)
        {
            var value = await redisDb.StringGetAsync(key);
            if (!value.IsNullOrEmpty)
                return JsonConvert.DeserializeObject<TEntity>(value);
            throw new NullReferenceException();
        }

        public async Task<bool> RemoveByKey(string key)
        {
            return await redisDb.KeyDeleteAsync(key);
        }

        public async Task<bool> Set(string key, TEntity value)
        {    
            return await redisDb.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }
    }
}
