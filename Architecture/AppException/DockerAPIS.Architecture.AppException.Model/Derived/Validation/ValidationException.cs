using DockerAPIS.Architecture.AppException.Model.Base;

namespace DockerAPIS.Architecture.AppException.Model.Derived.Validation
{
    public class ValidationException : BaseAppException
    {
        public ValidationException(params ValidationExceptionMessage[] messages) : base(messages) { }

        public ValidationException(string code)
        {
            this.Messages = new ValidationExceptionMessage[] { new ValidationExceptionMessage(code) };
        }
    }
}
