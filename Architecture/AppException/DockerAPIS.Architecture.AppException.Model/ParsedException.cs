using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Architecture.AppException.Model
{
    public record ParsedException
    {
        public HttpStatusCode StatusCode { get; set; }
      
        public string LogID { get; set; }
       
        public IEnumerable<string> Messages { get; set; }

        public ParsedException(HttpStatusCode statusCode, string logID, IEnumerable<string> messages)
        {
            StatusCode = statusCode;
            LogID = logID;
            Messages = messages;
        }
    }
}
