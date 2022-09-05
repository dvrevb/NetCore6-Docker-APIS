using DockerAPIS.Core.Caching.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.Core.Settings;
using DockerAPIS.DataAccess.Context;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.DataAccess.Caching
{
    public class RedisCachingBase<TEntity> : ICaching<TEntity> where TEntity : class, new()
    {
        private readonly RedisCacheContext _redisCacheContext;
        private readonly IDatabase _cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);
        public RedisCachingBase(IOptions<RedisSettings> settings)
        {
            _redisCacheContext = new RedisCacheContext(settings);
            _cache = _redisCacheContext.Connection.GetDatabase();
        }

        public async Task<GetManyResult<TEntity>> GetAllAsync()
        {
            var result = new GetManyResult<TEntity>();
            try
            {
                /*get all data*/
                var endPoint = _redisCacheContext.Connection.GetEndPoints().First();
                IServer server = _redisCacheContext.Connection.GetServer(endPoint);
                IEnumerable<string> keys = server.Keys().Select(key => (string)key).ToList();

                List<TEntity> values = new List<TEntity>();
                foreach (var item in keys)
                {
                    var value = JsonConvert.DeserializeObject<TEntity>(await _cache.StringGetAsync(item));
                    if (value != null)
                        values.Add(value);
                }

                if (values != null)
                    result.Result = values;

            }
            catch (Exception ex)
            {

                result.Message = $"GetAll {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }

        public async Task<GetOneResult<TEntity>> GetByIdAsync(string id)
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                var data = JsonConvert.DeserializeObject<TEntity>(await _cache.StringGetAsync(id));
                if (data != null)
                    result.Entity = data;

            }
            catch (Exception ex)
            {

                result.Message = $"GetById {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }

        public async Task<GetOneResult<TEntity>> InsertOneAsync(string id, TEntity entity)
        {
            var result = new GetOneResult<TEntity>();
            try
            {
              
                await _cache.StringSetAsync(id, JsonConvert.SerializeObject(entity));
                result.Entity = entity;

            }
            catch (Exception ex)
            {

                result.Message = $"InsertOne {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
    }
}
