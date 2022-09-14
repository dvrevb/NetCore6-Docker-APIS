using AutoMapper;
using DockerAPIS.Services.Classroom.ExternalData.Manager.Interfaces;
using DockerAPIS.Services.Classroom.ExternalData.Model.Contacts.GetContact;
using DockerAPIS.Services.Classroom.Manager.Business.Interface;
using DockerAPIS.Services.Classroom.Manager.Operation.Implementation;
using DockerAPIS.Services.Classroom.Manager.Operation.Interface;
using DockerAPIS.Services.Classroom.Model.DTO.Contacts;
using DockerAPIS.Services.Classroom.Model.Entity;
using DockerAPIS.Services.Classroom.Model.Exchange.AddLecture;
using DockerAPIS.Services.Classroom.Model.Exchange.AddStudent;
using DockerAPIS.Services.Classroom.Model.Exchange.GetLecture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Services.Classroom.Manager.Business.Implementation
{
    public class ClassroomBusinessManager : IClassroomBusinessManager
    {
        private readonly IContactDataStore contactDataStore;
        private readonly IMapper mapper;
        private readonly IClassroomOperations classroomCacheOperation;

        public ClassroomBusinessManager(IContactDataStore contactDataStore, IMapper mapper, IClassroomOperations classroomCacheOperation)
        {
            this.contactDataStore = contactDataStore;
            this.mapper = mapper;
            this.classroomCacheOperation = classroomCacheOperation;
        }
        public async Task<AddStudentResponseModel> AddStudent(AddStudentRequestModel model)
        {
            Lecture lecture = await classroomCacheOperation.GetByKey(model.LectureId);
            lecture.Students.Add(model.StudentId);
            var x = await classroomCacheOperation.Set(model.LectureId, lecture);
            return mapper.Map<AddStudentResponseModel>(lecture);
        }

        public async Task<AddLectureResponseModel> Create(AddLectureRequestModel model)
        {
            string key = Guid.NewGuid().ToString();
            Lecture lecture = mapper.Map<Lecture>(model);
            lecture.Students = new List<string>();
            await classroomCacheOperation.Set(key, lecture);
                var responseModel = mapper.Map<AddLectureResponseModel>(lecture);
            responseModel.Id = key;
            return responseModel;
        }

        public async Task<GetLectureResponseModel> Get(string id)
        {
            Lecture lecture = await classroomCacheOperation.GetByKey(id);
            
            var students = new List<ContactResult>();
            foreach (var item in lecture.Students)
            {
                students.Add(mapper.Map<ContactResult>(await contactDataStore.GetContact(item)));
            }
            var getLectureResponseModel = new GetLectureResponseModel { Name = lecture.Name, Students = students };
            return getLectureResponseModel;
        }
    }
}
