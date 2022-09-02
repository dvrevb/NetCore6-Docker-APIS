using Classroom.DTO;
using Classroom.Entities;
using Classroom.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Classroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public ClassroomController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpPost("AddStudent/{lectureId}/{personId}")]
        public async Task<IActionResult> AddStudent(string lectureId, string personId)
        {
            if (lectureId == null || personId == null)
                return NotFound();

            HttpClient client = new HttpClient();
            HttpResponseMessage personResponse = await client.GetAsync("http://contactsapi/api/Contacts/" + personId);

            var lecture = await _cacheService.GetValueAsync(lectureId);
            var lectureInfo = JsonConvert.DeserializeObject<Lecture>(lecture);

            if (!personResponse.IsSuccessStatusCode || lecture == null)
                return NotFound();

            lectureInfo?.Students.Add(personId);

            var result = await _cacheService.SetValueAsync(lectureId, JsonConvert.SerializeObject(lectureInfo));

            return (result == true) ? Ok() : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var lecture = await _cacheService.GetValueAsync(id);
            if (lecture == null)
                return NotFound();

            HttpClient client = new HttpClient();
            HttpResponseMessage personResponse;

            var lectureInfo = JsonConvert.DeserializeObject<Lecture>(lecture);
            if (lectureInfo != null)
            {          
                List<Student> studentList = new List<Student>();
                foreach (var item in lectureInfo.Students)
                {
                    personResponse = await client.GetAsync("http://contactsapi/api/Contacts/" + item);
                   
                    if (personResponse.IsSuccessStatusCode)
                    {
                        string person = await personResponse.Content.ReadAsStringAsync();
                        var studentDateOfBirth = JObject.Parse(person)["DateOfBirth"];
                        var studentName = JObject.Parse(person)["Name"];
                        int age;
                        if (studentName != null && studentDateOfBirth != null)
                        {
                            age = DateTime.Now.Year - Convert.ToDateTime(studentDateOfBirth).Year;
                            studentList.Add(new Student { Name = studentName.ToString(), Age = age }); ;
                        }
                    }
                }
                LectureDto dto = new LectureDto { LectureName = lectureInfo.Name, Students = studentList };
                return Ok(dto);
            }
            return NotFound();
        }

        [HttpPost("Add/{Name}")]
        public async Task<IActionResult> AddAsync(string Name)
        {
            var id = Guid.NewGuid().ToString();
            List<string> students = new List<string>();
            Lecture c = new Lecture { Name = Name, Students = students };
            await _cacheService.SetValueAsync(id, JsonConvert.SerializeObject(c));
            return Ok();
        }
    }
}
