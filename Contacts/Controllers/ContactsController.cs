using Contacts.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Contacts.Services.Abstract;
using System.Xml.Linq;
using Contacts.DTO;

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
        public async Task<IActionResult> GetAll()
        {
            var keys = _cacheService.GetAll();
            var names = new List<string>();
            foreach (var item in keys)
            {
                var value = await _cacheService.GetValueAsync(item);
                var contact = JsonConvert.DeserializeObject<Contact>(value);

                if (contact != null)
                    names.Add(contact.Name);
            }
            return Ok(names);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var contact = JsonConvert.DeserializeObject<Contact>(await _cacheService.GetValueAsync(id));
            if (contact == null)
                return NotFound();

            ContactDto c = new ContactDto { Name = contact.Name, Age = DateTime.Now.Year - contact.DateOfBirth.Year };
            return Ok(c);
        }

        [HttpPost("{name}/{dateOfBirth}")]
        public async Task<IActionResult> AddAsync(string name, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name))
                return NotFound();

            var id = Guid.NewGuid().ToString();
            Contact c = new Contact { Name = name, DateOfBirth = dateOfBirth };
            await _cacheService.SetValueAsync(id, JsonConvert.SerializeObject(c));
            return Ok(id);
        }
    }
}
