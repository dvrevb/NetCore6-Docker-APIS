using Contacts.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Contacts.Services.Abstract;
using System.Xml.Linq;
using DockerAPIS.Business.Abstract;
using Newtonsoft.Json.Linq;
using System;
using DockerAPIS.Entities.DTO;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetAllAsync();
            if (!result.Success)
                return NotFound();

            List<string> names = new List<string>();

            foreach (var item in result.Result)
            {
                names.Add(item.Name);
            }

            return Ok(names);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _contactService.GetAsync(id);
            if (!result.Success)
                return NotFound();   
            ContactDto c = new ContactDto { Name = result.Entity.Name, Age = DateTime.Now.Year - result.Entity.DateOfBirth.Year };
            return Ok(c);

        }

        [HttpPost("{name}/{dateOfBirth}")]
        public async Task<IActionResult> AddAsync(string name, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name))
                return NotFound();

            var id = Guid.NewGuid().ToString();
            Contact c = new Contact { Name = name, DateOfBirth = dateOfBirth };
            var result = await _contactService.CreateAsync(id, c);
            if (!result.Success)
                return BadRequest();
            return Ok(id);
        }
    }
}
