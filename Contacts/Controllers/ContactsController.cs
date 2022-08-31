using Contacts.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Contacts.Services.Abstract;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ICacheService _cacheService;

        public ContactsController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok( _cacheService.GetAll().ToList());        
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cacheService.GetValueAsync(id));
        }

        [HttpPost("Add/{Name}/{DateOfBirth}")]
        public async Task<IActionResult> AddAsync(string? Name, DateTime? DateOfBirth)
        {
            var id = Guid.NewGuid().ToString();
            Contact c = new Contact { Name = Name, DateOfBirth = DateOfBirth };
            await _cacheService.SetValueAsync(id, JsonConvert.SerializeObject(c));
            return Ok();
        }
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return View();
        }
    }
}
