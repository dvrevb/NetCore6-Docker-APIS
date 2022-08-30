using Contacts.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Pipelines.Sockets.Unofficial.Buffers;
using StackExchange.Redis;
using System.Text;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IDistributedCache _cache;

        public ContactsController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public  IActionResult Index()
        {
            return View();
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string? id)
        {
            var value = Encoding.UTF8.GetString(_cache.Get(id));
            //var value = await _cache.GetAsync(id.ToString());
            return string.IsNullOrEmpty(value.ToString()) ? NotFound() : Ok(JsonConvert.SerializeObject(value));
        }

        [HttpPost("Add/{Name}/{DateOfBirth}")]
        public async Task<IActionResult> AddAsync(string? Name, DateTime? DateOfBirth)
        {
             var id = Guid.NewGuid().ToString();
             var options = new DistributedCacheEntryOptions
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                 SlidingExpiration = TimeSpan.FromMinutes(10)
             };

            Contact c = new Contact { Name = Name, DateOfBirth = DateOfBirth };
            await _cache.SetStringAsync(id, JsonConvert.SerializeObject(c), options);

            //await _cache.SetAsync("zaman", Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
            return Ok(_cache.GetStringAsync(id));
        }
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            /* var id = Guid.NewGuid().ToString();
             var options = new DistributedCacheEntryOptions
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                 SlidingExpiration = TimeSpan.FromMinutes(10)
             };*/

            // Contact c = new Contact { Name = Name, DateOfBirth = DateOfBirth };
            //await _cache.SetStringAsync(id, JsonConvert.SerializeObject(c), options);

            await _cache.RemoveAsync(id);
            return Ok(Encoding.UTF8.GetString(_cache.Get(id)));
        }
    }
}
