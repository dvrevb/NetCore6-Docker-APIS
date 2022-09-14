using DockerAPIS.Architecture.Data.Cache.Base;
using DockerAPIS.Services.Classroom.Model.Entity;

namespace DockerAPIS.Services.Classroom.Manager.Operation.Interface
{
    public interface IClassroomOperations 
    {
        public Task<IEnumerable<Lecture>> GetAll();
        public Task<Lecture> GetByKey(string key);
        public Task<bool> RemoveByKey(string key);
        public Task<bool> Set(string key, Lecture value);
       
    }
}
