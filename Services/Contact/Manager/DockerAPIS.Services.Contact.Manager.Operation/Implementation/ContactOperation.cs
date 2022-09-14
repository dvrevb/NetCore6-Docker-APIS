using DockerAPIS.Architecture.Data.Cache.Base;
using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Services.Contact.Manager.Operation.Interfaces;
using DockerAPIS.Services.Contact.Model.Entity;
using StackExchange.Redis;

namespace DockerAPIS.Services.Contact.Manager.Operation.Implementation
{
    public class ContactOperation : IContactOperation
    {
        private readonly ICache<Person> cache;
        public ContactOperation(ICache<Person> cache)
        {
            this.cache = cache;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await cache.GetAll();
        }

        public async Task<Person> GetByKey(string key)
        {
            return await cache.GetByKey(key);
        }

        public async Task<bool> RemoveByKey(string key)
        {
            return await cache.RemoveByKey(key);
        }

        public async Task<bool> Set(string key, Person value)
        {
            return await cache.Set(key, value);
        }
    }
}
