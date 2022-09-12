using DockerAPIS.Services.Classroom.Model.Exchange.AddLecture;
using DockerAPIS.Services.Classroom.Model.Exchange.AddStudent;
using DockerAPIS.Services.Classroom.Model.Exchange.GetLecture;

namespace DockerAPIS.Services.Classroom.Manager.Business.Interface
{
    public interface IClassroomBusinessManager
    {
        Task<GetLectureResponseModel> Get(string id);
        Task<AddLectureResponseModel> Create(AddLectureRequestModel model);
        Task<AddStudentResponseModel> AddStudent(AddStudentRequestModel model);
    }
}
