using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Services.Contact.Manager.Operation.Interfaces;
using DockerAPIS.Services.Contact.Model.Entity;
using StackExchange.Redis;

namespace DockerAPIS.Services.Contact.Manager.Operation.Implementation
{
    public class ContactOperation : RedisCaching<Person>, IContactOperation
    {
        public ContactOperation(IDatabase redisDb, IConnectionMultiplexer multiplexer) : base(redisDb, multiplexer)
        {
        }
    }
}
