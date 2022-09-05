using DockerAPIS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Core.Caching.Abstract
{
    public interface ICaching<TEntity> where TEntity : class, new()
    {
        Task<GetManyResult<TEntity>> GetAllAsync();
        Task<GetOneResult<TEntity>> GetByIdAsync(string id);
        Task<GetOneResult<TEntity>> InsertOneAsync(string id,TEntity entity);
    }
}
