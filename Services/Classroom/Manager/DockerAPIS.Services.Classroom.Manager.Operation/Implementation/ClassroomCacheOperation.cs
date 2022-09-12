using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Services.Classroom.Manager.Operation.Interface;
using DockerAPIS.Services.Classroom.Model.Entity;
using StackExchange.Redis;

namespace DockerAPIS.Services.Classroom.Manager.Operation.Implementation
{
    public class ClassroomCacheOperation : RedisCaching<Lecture>, IClassroomCacheOperation
    {
        public ClassroomCacheOperation(IDatabase redisDb, IConnectionMultiplexer multiplexer) : base(redisDb, multiplexer)
        {
        }
    }
}
