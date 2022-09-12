using DockerAPIS.Architecture.Data.Cache.Base;
using DockerAPIS.Services.Classroom.Model.Entity;

namespace DockerAPIS.Services.Classroom.Manager.Operation.Interface
{
    public interface IClassroomCacheOperation : ICache<Lecture>
    {
    }
}
