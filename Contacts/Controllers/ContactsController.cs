
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Contacts.Services.Abstract;
using System.Xml.Linq;
using DockerAPIS.Business.Abstract;
using Newtonsoft.Json.Linq;
using System;
using DockerAPIS.Entities.DTO;
using DockerAPIS.Entities;

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
            return (result!=null) ? Ok(result.Result.Select(x => x.Name)) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _contactService.GetAsync(id);
            if (!result.Success)
                return NotFound();   
            return Ok(result.Entity);
        }

        [HttpPost("{name}/{dateOfBirth}")]
        public async Task<IActionResult> AddAsync(string name, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name))
                return NotFound();

            var id = Guid.NewGuid().ToString();
            var result = await _contactService.CreateAsync(id, name, dateOfBirth);

            return result.Success ? Ok(id) : BadRequest();
        }
    }
}
