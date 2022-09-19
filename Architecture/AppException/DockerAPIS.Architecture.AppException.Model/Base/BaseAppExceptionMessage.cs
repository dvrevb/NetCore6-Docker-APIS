using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Architecture.AppException.Model.Base
{
    public abstract record BaseAppExceptionMessage
    {
        public string Code { get; set; }
        public string? Message { get; set; }

        protected BaseAppExceptionMessage(string code)
        {
            Code = code;
        }

        protected BaseAppExceptionMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
