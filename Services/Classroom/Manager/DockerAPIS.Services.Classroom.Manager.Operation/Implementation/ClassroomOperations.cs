using DockerAPIS.Architecture.Data.Cache.Base;
using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Services.Classroom.Manager.Operation.Interface;
using DockerAPIS.Services.Classroom.Model.Entity;
using StackExchange.Redis;

namespace DockerAPIS.Services.Classroom.Manager.Operation.Implementation
{
    public class ClassroomOperations : IClassroomOperations
    {
        private readonly ICache<Lecture> cache;
        public ClassroomOperations(ICache<Lecture> cache)
        {
            this.cache = cache;
        }

        public async Task<IEnumerable<Lecture>> GetAll()
        {
            return await cache.GetAll();
        }

        public async Task<Lecture> GetByKey(string key)
        {
            return await cache.GetByKey(key);
        }

        public async Task<bool> RemoveByKey(string key)
        {
          return await cache.RemoveByKey(key);
        }

        public async Task<bool> Set(string key, Lecture value)
        {
            return await cache.Set(key, value);
        }
    }
}
