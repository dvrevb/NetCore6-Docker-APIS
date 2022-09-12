using DockerAPIS.Services.Contact.Model.Entity;
using DockerAPIS.Architecture.Data.Cache.Base;

namespace DockerAPIS.Services.Contact.Manager.Operation.Interfaces
{
    public interface IContactCacheOperation :  ICache<Person>
    {
    }
}
