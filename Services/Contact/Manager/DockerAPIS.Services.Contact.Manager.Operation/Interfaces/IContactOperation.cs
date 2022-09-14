using DockerAPIS.Services.Contact.Model.Entity;
using DockerAPIS.Architecture.Data.Cache.Base;

namespace DockerAPIS.Services.Contact.Manager.Operation.Interfaces
{
    public interface IContactOperation
    {
        public Task<IEnumerable<Person>> GetAll();
        public Task<Person> GetByKey(string key);
        public Task<bool> RemoveByKey(string key);
        public Task<bool> Set(string key, Person value);
    }
}
