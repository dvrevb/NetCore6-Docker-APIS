using DockerAPIS.Architecture.AppException.Model.Base;

namespace DockerAPIS.Architecture.AppException.Model.Derived.Business
{
    public record BusinessExceptionMessage : BaseAppExceptionMessage
    {
        public BusinessExceptionMessage(string code) : base(code) { }
    }
}
