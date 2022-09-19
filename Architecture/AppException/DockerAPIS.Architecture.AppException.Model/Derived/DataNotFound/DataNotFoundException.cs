using DockerAPIS.Architecture.AppException.Model.Base;
using System;

namespace DockerAPIS.Architecture.AppException.Model.Derived.DataNotFound
{
    public class DataNotFoundException : BaseAppException
    {
        public DataNotFoundException(params DataNotFoundExceptionMessage[] messages) : base(messages) { }

        public DataNotFoundException(string entityName, string value)
        {
            this.Messages = new DataNotFoundExceptionMessage[] { new DataNotFoundExceptionMessage(null, entityName, value) };
        }

        public DataNotFoundException(string entityName, Guid value)
        {
            this.Messages = new DataNotFoundExceptionMessage[] { new DataNotFoundExceptionMessage(null, entityName, value.ToString()) };
        }
    }
}
