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

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var value = _cache.Get(id);
            var lecture = JsonConvert.DeserializeObject(await _cacheService.GetValueAsync(id));
            return Ok(await _cacheService.GetValueAsync(id));

            //var value = await _cache.GetAsync(id.ToString());
            //return string.IsNullOrEmpty(value.ToString()) ? NotFound() : Ok(JsonConvert.SerializeObject(value));
        }

        [HttpPost("Add/{Name}")]
        public async Task<IActionResult> AddAsync(string Name)
        {
            /*  var id = Guid.NewGuid().ToString();
               var options = new DistributedCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                   SlidingExpiration = TimeSpan.FromMinutes(10)
               };

              Contact c = new Contact { Name = Name, DateOfBirth = DateOfBirth };
              await _cache.SetStringAsync("deneme", JsonConvert.SerializeObject(c), options);
              //await _cache.SetStringAsync("deneme1", "bu bir denemedir2", options);

              //await _cache.SetAsync("zaman", Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
              return Ok(id);*/
            var id = Guid.NewGuid().ToString();
            List<string> students = new List<string>();
            Lecture c = new Lecture { Name = Name, Students = students};
            await _cacheService.SetValueAsync(id, JsonConvert.SerializeObject(c));
            return Ok();
        }
    }
}
