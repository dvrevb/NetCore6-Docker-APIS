
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.DataAccess.Concrete;
using DockerAPIS.Entities;
using DockerAPIS.Entities.DTO;
using Microsoft.Extensions.Configuration;
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
    public static class LectureConstantKeys
    {
        public const string MaximumStudentCount = "MaximumStudentCount";
    }

    public interface ILectureConstantsStore
    {
        int MaximumStudentCount { get; }
    }

    public abstract class ApplicationSettingsStore
    {
        private readonly IConfiguration Configuration;

        public ApplicationSettingsStore(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected T GetValue<T>(string key)
        {
            return (T)Convert.ChangeType(this.Configuration[key], typeof(T));
        }
    }

    public class LectureConstantsApplicationSettingsStore : ApplicationSettingsStore, ILectureConstantsStore
    {
        public LectureConstantsApplicationSettingsStore(IConfiguration configuration) : base(configuration)
        {
        }

        public int MaximumStudentCount
        {
            get
            {
                return base.GetValue<int>(LectureConstantKeys.MaximumStudentCount);
            }
        }
    }

    public class ClassroomManager : IClassroomService
    {
        private readonly IClassroomDataAccess _classroomDataAccess;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILectureConstantsStore LectureConstantsStore;

        public ClassroomManager(IClassroomDataAccess classroomDataAccess, IHttpClientFactory httpClientFactory, ILectureConstantsStore lectureConstantsStore)
        {
            _httpClientFactory = httpClientFactory;
            _classroomDataAccess = classroomDataAccess;
            this.LectureConstantsStore = lectureConstantsStore;
        }

        public async Task<GetOneResult<AddStudentResponseModel>> AddStudent(AddStudentRequestModel request)
        {
            var lecture = await _classroomDataAccess.GetByIdAsync(lectureId);

            if (lecture.CurrentStudentCount >= this.LectureConstantsStore.MaximumStudentCount)
            {
                throw new Exception("aaaa");
            }

            var response = await _httpClientFactory.CreateClient()
                .GetAsync("http://contactsapi/api/Contacts/" + studentId);

            if (response.IsSuccessStatusCode == false || lecture.Success == false)
            {
                lecture.Success = false;
                return lecture;
            }

            var student = await this.ContactDataStore.GetContact(contactID);
            if (student is null)
            {
                return lecture;
            }

            lecture.Entity.Students.Add(studentId);

            var result = await _classroomDataAccess.InsertOneAsync(lectureId, lecture.Entity);

            if (lecture.CurrentStudentCount >= LectureConstants.MaximumStudentCount)
            {
                result.Warning = "bir şey bir şey";
            }

            return result;
        }

        public async Task<GetOneResult<Lecture>> CreateAsync(string id, string name)
        {
            Lecture lecture = new(name);
            GetOneResult<Lecture> result = await _classroomDataAccess.InsertOneAsync(id, lecture);
            return result;
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            GetManyResult<Lecture> result = await _classroomDataAccess.GetAllAsync();
            return result;
        }

        public async Task<LectureDto> GetAsync(string id)
        {
            Lecture lecture = await this._classroomDataAccess.GetByIdAsync(id) ?? throw new DataNotFoundException(nameof(lecture), id);

            var lecture = await _classroomDataAccess.GetByIdAsync(id);
            var lectureDto = new GetOneResult<LectureDto>();
            if (lecture.Success == false)
            {
                lectureDto.Success = false;
                return lectureDto;
            }

            var client = _httpClientFactory.CreateClient();

            List<Student> studentList = new List<Student>();

            if (lecture?.Entity?.Students?.Count > 0)
            {
                foreach (var item in lecture?.Entity?.Students)
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
            }

            foreach (var item in lecture?.Entity?.Students ?? Array.Empty<Student>())
            {
                var contactResponse = await client.GetAsync("http://contactsapi/api/Contacts/" + item);

                if (!contactResponse.IsSuccessStatusCode)
                {
                    continue;
                }
                
                studentList.AddSafe(await contactResponse.Content.ReadFromJsonAsync<Student>());
            }

            lectureDto.Entity = new LectureDto { LectureName = lecture.Entity.Name, Students = studentList };
            return lectureDto;
        }
    }

    public static class ListExtensions
    {
        public static void AddSafe<T>(this List<T> list, T element)
        {
            if (element is null) return;
            list.Add(element);
        }
    }
}
