using DockerAPIS.Architecture.AppException.Model;
using DockerAPIS.Architecture.AppException.Model.Derived.Business;
using DockerAPIS.Architecture.AppException.Model.Derived.DataNotFound;
using DockerAPIS.Architecture.AppException.Model.Derived.Validation;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;


namespace DockerAPIS.Architecture.AppException.Manager
{
    public class ExceptionParser
    {
        private readonly IDatabase Cache;
        private readonly string CacheRootKey = "ErrorMessages:";

        public ExceptionParser(IDatabase cache)
        {
            this.Cache = cache;            
        }

        public ParsedException Parse(Exception exception)
        {
            return exception switch
            {
                BusinessException businessException => ParseBusinessException(businessException),
                DataNotFoundException dataNotFoundException => ParseDataNotFoundException(dataNotFoundException),
                ValidationException validationException => ParseValidationException(validationException),
                _ => ParseUnhandledException()
            };
        }

        private ParsedException ParseBusinessException(BusinessException businessException)
        {
            return new ParsedException(HttpStatusCode.UnprocessableEntity, null, businessException.Messages?.Select(message => this.Cache.StringGet($"{this.CacheRootKey}{message.Code}").ToString()));
        }

        private ParsedException ParseDataNotFoundException(DataNotFoundException dataNotFoundException)
        {
            return new ParsedException(HttpStatusCode.BadRequest, null, dataNotFoundException.Messages?.Select(message => 
            {
                DataNotFoundExceptionMessage dataNotFoundExceptionMessage = message as DataNotFoundExceptionMessage;
                return String.Format(this.Cache.StringGet($"{this.CacheRootKey}{"DataNotFound"}"), dataNotFoundExceptionMessage.Value, dataNotFoundExceptionMessage.EntityName);
            }));
        }

        private ParsedException ParseValidationException(ValidationException validationException)
        {
            return new(HttpStatusCode.BadRequest, null, validationException.Messages?.Select(message =>
            {
                ValidationExceptionMessage validationMessage = message as ValidationExceptionMessage;
                return String.Format(this.Cache.StringGet($"{this.CacheRootKey}{validationMessage.Code}"), validationMessage.FieldName, validationMessage.MaxValue ?? validationMessage.MinValue);
            }));
        }

        private ParsedException ParseUnhandledException()
        {
            return new(HttpStatusCode.InternalServerError, Guid.NewGuid().ToString(), new string[] { this.Cache.StringGet($"{this.CacheRootKey}{"UnhandledException"}").ToString() });
        }
    }
}
