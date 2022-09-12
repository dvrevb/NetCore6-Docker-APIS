using AutoMapper;
using DockerAPIS.Services.Classroom.ExternalData.Model.Contacts.GetContact;
using DockerAPIS.Services.Classroom.Model.DTO.Contacts;
using DockerAPIS.Services.Classroom.Model.Entity;
using DockerAPIS.Services.Classroom.Model.Exchange.AddLecture;
using DockerAPIS.Services.Classroom.Model.Exchange.AddStudent;
using DockerAPIS.Services.Classroom.Model.Exchange.GetLecture;

namespace DockerAPIS.Services.Classroom.Manager.Mapper
{
    public class ClassroomMappingProfile : Profile
    {
        public ClassroomMappingProfile()
        {
            CreateMap<AddLectureRequestModel, Lecture>();
            CreateMap<Lecture, AddLectureResponseModel>();
            CreateMap<Lecture, AddLectureResponseModel>();
            //CreateMap<Lecture, GetLectureResponseModel>();
            CreateMap<GetContactResponseModel, ContactResult>();
            CreateMap<Lecture, AddStudentResponseModel>();
        }
    }
}
