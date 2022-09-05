using Classroom.Entities;
using Classroom.Services.Abstract;
using Contacts.Entities;
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.Entities;
using DockerAPIS.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

namespace Classroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClassroomController(IClassroomService classroomService, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _classroomService = classroomService;
        }

        [HttpPost("AddStudent/{lectureId}/{personId}")]
        public async Task<IActionResult> AddStudent(string lectureId, string personId)
        {
            if (lectureId == null || personId == null)
                return NotFound();

            var lecture = await _classroomService.GetAsync(lectureId);

            var response = await _httpClientFactory.CreateClient()
                .GetAsync("http://contactsapi/api/Contacts/" + personId);         
           
            if (!response.IsSuccessStatusCode || lecture==null || lecture.Success==false)
                return NotFound();

            lecture.Entity.Students.Add(personId);

            var result = await _classroomService.CreateAsync(lectureId,lecture.Entity);
            return (result.Success == true) ? Ok(result.Entity) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var lecture = await _classroomService.GetAsync(id);

            if (lecture == null || lecture.Success== false)
                return NotFound();

            var client = _httpClientFactory.CreateClient();

            List<Student> studentList = new List<Student>();

            foreach (var item in lecture.Entity.Students)
            {
                var contactResponse = await client.GetAsync("http://contactsapi/api/Contacts/" + item);
                if (contactResponse.IsSuccessStatusCode)
                {
                    var contact = JsonConvert.DeserializeObject<Contact>(await contactResponse.Content.ReadAsStringAsync());
                    if (contact!= null)
                    {
                        studentList.Add(new Student
                        { Name = contact.Name, Age = DateTime.Now.Year - contact.DateOfBirth.Year });
                    }
                }
            }
            LectureDto dto = new LectureDto { LectureName = lecture.Entity.Name ,Students = studentList };
            return Ok(dto);
        }

        [HttpPost("Add/{Name}")]
        public async Task<IActionResult> AddAsync(string Name)
        {
            var id = Guid.NewGuid().ToString();
            List<string> students = new List<string>();
            Lecture c = new Lecture { Name = Name, Students = students };
            var result = await _classroomService.CreateAsync(id,c);
            return Ok(id);
        }
    }
}
