using Classroom.Entities;
using Classroom.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpPost("AddStudent/{id}")]
        public async Task<IActionResult> AddStudent(string id)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://webapi/Counter");
            var lecture = JsonConvert.DeserializeObject(await _cacheService.GetValueAsync(id));
            return Ok(await _cacheService.GetValueAsync(id));
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var value = _cache.Get(id);
            var lecture = JsonConvert.DeserializeObject(await _cacheService.GetValueAsync(id));
            return Ok(await _cacheService.GetValueAsync(id));
        }

        [HttpPost("Add/{Name}")]
        public async Task<IActionResult> AddAsync(string Name)
        {

            var id = Guid.NewGuid().ToString();
            List<string> students = new List<string>();
            Lecture c = new Lecture { Name = Name, Students = students};
            await _cacheService.SetValueAsync(id, JsonConvert.SerializeObject(c));
            return Ok();
        }
    }
}
