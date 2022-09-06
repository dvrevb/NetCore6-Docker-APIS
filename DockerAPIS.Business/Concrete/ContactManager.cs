
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.Entities;
using DockerAPIS.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DockerAPIS.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDataAccess _contactDataAccess;

        public ContactManager(IContactDataAccess contactDataAccess)
        {
            _contactDataAccess = contactDataAccess;
        }

        public async Task<GetOneResult<Contact>> CreateAsync(string id, string name, DateTime dateOfBirth)
        {           
            Contact c = new Contact { Name = name, DateOfBirth = dateOfBirth };
            var result = await _contactDataAccess.InsertOneAsync(id, c);
            return result;
        }

        public async Task<GetManyResult<Contact>> GetAllAsync()
        {
            GetManyResult<Contact> result = await _contactDataAccess.GetAllAsync();
            return result;
        }

        public async Task<GetOneResult<ContactDto>> GetAsync(string id)
        {
            GetOneResult<ContactDto> contactDto = new GetOneResult<ContactDto>();
            var result = await _contactDataAccess.GetByIdAsync(id);
            if (!result.Success)
            {
                contactDto.Success = false;
                contactDto.Message = "GetAsync - NotFound";
            }
            contactDto.Entity = new ContactDto { Name = result.Entity.Name, Age = DateTime.Now.Year - result.Entity.DateOfBirth.Year };

            return contactDto;
        }
    }
}
