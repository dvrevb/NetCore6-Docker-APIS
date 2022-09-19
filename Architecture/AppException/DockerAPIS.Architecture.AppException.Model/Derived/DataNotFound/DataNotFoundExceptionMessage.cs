using DockerAPIS.Architecture.AppException.Model.Base;

namespace DockerAPIS.Architecture.AppException.Model.Derived.DataNotFound
{
    public record DataNotFoundExceptionMessage : BaseAppExceptionMessage
    {
        public string EntityName { get; set; }
        public string Value { get; set; }

        public DataNotFoundExceptionMessage(string code) : base(code) { }

        public DataNotFoundExceptionMessage(string code, string entityName, string value) : base(code)
        {
            this.EntityName = entityName;
            this.Value = value;
        }
    }
}
