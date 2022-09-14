using DockerAPIS.Architecture.Data.Cache.Concrete.Redis;
using DockerAPIS.Services.Classroom.Manager.Operation.Interface;
using DockerAPIS.Services.Classroom.Model.Entity;
using StackExchange.Redis;

namespace DockerAPIS.Services.Classroom.Manager.Operation.Implementation
{
    public class ClassroomOperations : RedisCaching<Lecture>, IClassroomOperations
    {
        public ClassroomOperations(IDatabase redisDb, IConnectionMultiplexer multiplexer) : base(redisDb, multiplexer)
        {
        }
    }
}
