using Contacts.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Pipelines.Sockets.Unofficial.Buffers;
using StackExchange.Redis;
using System.Text;
using StackExchange.Redis;
using Contacts.Services.Abstract;
using System.Reflection;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        //private readonly IDistributedCache _cache;
        private readonly ICacheService _cacheService;

        public ContactsController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("GetAll")]
        public  IActionResult GetAll()
        {
            return Ok( _cacheService.GetAllAsync().ToList());        
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var value = _cache.Get(id);

            return Ok(await _cacheService.GetValueAsync(id));

            //var value = await _cache.GetAsync(id.ToString());
            //return string.IsNullOrEmpty(value.ToString()) ? NotFound() : Ok(JsonConvert.SerializeObject(value));
        }

        [HttpPost("Add/{Name}/{DateOfBirth}")]
        public async Task<IActionResult> AddAsync(string? Name, DateTime? DateOfBirth)
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
            Contact c = new Contact { Name = Name, DateOfBirth = DateOfBirth };
            await _cacheService.SetValueAsync(id, JsonConvert.SerializeObject(c));
            return Ok();
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

            //await _cache.RemoveAsync(id);
            //return Ok(Encoding.UTF8.GetString(_cache.Get(id)));
            return View();
        }
    }
}
