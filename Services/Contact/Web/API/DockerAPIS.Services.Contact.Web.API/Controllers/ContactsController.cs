using DockerAPIS.Architecture.AppException.Model;
using DockerAPIS.Services.Contact.Manager.Business.Interface;
using DockerAPIS.Services.Contact.Model.Exchange.AddContact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DockerAPIS.Services.Contact.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactBusinessManager contactBusinessManager;

        public ContactsController(IContactBusinessManager contactBusinessManager)
        {
            this.contactBusinessManager = contactBusinessManager;
        }
       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await contactBusinessManager.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ParsedException))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await contactBusinessManager.Get(id));   
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContactRequestModel model)
        {
            return Ok(await contactBusinessManager.Create(model));
        }
    }
}
