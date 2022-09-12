namespace DockerAPIS.Services.Classroom.Model.Exchange.AddStudent
{
    public class AddStudentResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<string> Students { get; set; }
    }
}
