using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Architecture.AppException.Model.Base
{
    public abstract class BaseAppException : ApplicationException
    {
        public IEnumerable<BaseAppExceptionMessage> Messages { get; set; }
        public BaseAppException(params BaseAppExceptionMessage[] messages)
        {
            this.Messages = messages;
        }
    }
}
