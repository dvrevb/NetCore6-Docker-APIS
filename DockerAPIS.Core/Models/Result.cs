using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Core.Models
{
    public class Result
    {
        public Result()
        {
            Success = true;
            Message = "";
        }
        public bool Success { get; set; }
        public string Message { get; set; }

    }


    /*Tentity must be class and newable*/
    public class GetOneResult<TEntity> : Result where TEntity : class, new()
    {
        public TEntity Entity { get; set; }
    }
    public class GetManyResult<TEntity> : Result where TEntity : class, new()
    {
        public IEnumerable<TEntity> Result { get; set; }
    }
}
