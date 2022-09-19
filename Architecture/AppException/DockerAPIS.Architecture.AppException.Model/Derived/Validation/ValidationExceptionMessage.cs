using DockerAPIS.Architecture.AppException.Model.Base;

namespace DockerAPIS.Architecture.AppException.Model.Derived.Validation
{
    public record ValidationExceptionMessage : BaseAppExceptionMessage
    {
        public string FieldName { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }

        public ValidationExceptionMessage(string code) : base(code) { }

        public ValidationExceptionMessage(string code, string fieldName, string minValue = null, string maxValue = null) : base(code) 
        {
            this.FieldName = fieldName;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
