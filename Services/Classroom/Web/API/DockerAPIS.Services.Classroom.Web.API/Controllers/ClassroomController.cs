using DockerAPIS.Services.Classroom.Manager.Business.Interface;
using DockerAPIS.Services.Classroom.Model.Exchange.AddLecture;
using DockerAPIS.Services.Classroom.Model.Exchange.AddStudent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DockerAPIS.Services.Classroom.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        IClassroomBusinessManager classroomBusinessManager;
        public ClassroomController(IClassroomBusinessManager classroomBusinessManager)
        {
            this.classroomBusinessManager = classroomBusinessManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentRequestModel model)
        {
            return Ok(await classroomBusinessManager.AddStudent(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await classroomBusinessManager.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLectureRequestModel model)
        {
           return Ok(await classroomBusinessManager.Create(model));
        }
    }
}
