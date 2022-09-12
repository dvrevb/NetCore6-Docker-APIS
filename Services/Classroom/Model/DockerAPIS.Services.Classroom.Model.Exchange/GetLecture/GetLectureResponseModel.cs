using DockerAPIS.Services.Classroom.Model.DTO.Contacts;

namespace DockerAPIS.Services.Classroom.Model.Exchange.GetLecture
{
    public class GetLectureResponseModel
    {
        public string Name { get; set; }
        public IEnumerable<ContactResult> Students { get; set; }
    }
}
