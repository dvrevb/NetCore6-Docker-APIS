using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Architecture.Data.Cache.Base
{
    public interface ICache<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetByKey(string key);
        public Task<bool> Set(string key, TEntity value);
        public Task<bool> RemoveByKey(string key);
    }
}
