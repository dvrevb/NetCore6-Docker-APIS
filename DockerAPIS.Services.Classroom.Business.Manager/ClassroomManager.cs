
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.DataAccess.Concrete;
using DockerAPIS.Entities;
using DockerAPIS.Entities.DTO;
using DockerAPIS.Services.Classroom.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DockerAPIS.Business.Concrete
{
    public class ClassroomManager : IClassroomService
    {
        private readonly IClassroomDataAccess _classroomDataAccess;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IContactDataStore ContanctDataStore;

        public ClassroomManager(IClassroomDataAccess classroomDataAccess, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _classroomDataAccess = classroomDataAccess;
        }

        public async Task<GetOneResult<Lecture>> AddStudent(string lectureId, string studentId)
        {
            var lecture = await _classroomDataAccess.GetByIdAsync(lectureId);

            var response = await _httpClientFactory.CreateClient()
                .GetAsync("http://contactsapi/api/Contacts/" + studentId);

            if (!response.IsSuccessStatusCode || lecture.Success == false)
            {
                lecture.Success = false;
                return lecture;
            }
              
            lecture.Entity.Students.Add(studentId);

            var result = await _classroomDataAccess.InsertOneAsync(lectureId, lecture.Entity);
            return result;
        }

        public async Task<GetOneResult<Lecture>> CreateAsync(string id, string name)
        {
            List<string> students = new List<string>();
            Lecture lecture = new Lecture { Name = name, Students = students };
            GetOneResult<Lecture> result = await _classroomDataAccess.InsertOneAsync(id, lecture);
            return result;
        }

        public async Task<GetManyResult<Lecture>> GetAllAsync()
        {
            GetManyResult<Lecture> result = await _classroomDataAccess.GetAllAsync();
            return result;
        }

        public async Task<GetOneResult<LectureDto>> GetAsync(string id)
        {
            var lecture = await _classroomDataAccess.GetByIdAsync(id);
            var lectureDto = new GetOneResult<LectureDto>();
            if (lecture.Success == false)
            {
                lectureDto.Success = false;
                return lectureDto;
            }

            var client = _httpClientFactory.CreateClient();

            List<Student> studentList = new List<Student>();

            foreach (var item in lecture.Entity.Students)
            {
                var contactResponse = await client.GetAsync("http://contactsapi/api/Contacts/" + item);
                if (contactResponse.IsSuccessStatusCode)
                {
                    var student = await contactResponse.Content.ReadFromJsonAsync<Student>();
                    if (student != null)
                    {
                        studentList.Add(student);
                    }
                }
            }

            lectureDto.Entity = new LectureDto { LectureName = lecture.Entity.Name, Students = studentList };     
            return lectureDto;
        }
    }
}
