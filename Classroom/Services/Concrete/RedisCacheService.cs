using Classroom.Services.Abstract;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using StackExchange.Redis;
using System.Collections;
using System.Linq;

namespace Classroom.Services.Concrete
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }

        public IEnumerable<string> GetAll()
        {
            var endPoint = _redisCon.GetEndPoints().First();
            IServer server = _redisCon.GetServer(endPoint);
            IEnumerable<string> keys = server.Keys().Select(key => (string)key).ToList();
            return keys;
        }
        public async Task<string> GetValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _cache.StringSetAsync(key, value, ExpireTime);
        }
    }
}
