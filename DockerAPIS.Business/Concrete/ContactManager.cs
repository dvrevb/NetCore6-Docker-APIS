
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDataAccess _contactDataAccess;

        public ContactManager(IContactDataAccess contactDataAccess)
        {
            _contactDataAccess = contactDataAccess;
        }

        public async Task<GetOneResult<Contact>> CreateAsync(string id, Contact contact)
        {
            GetOneResult<Contact> result = await _contactDataAccess.InsertOneAsync(id,contact);
            return result;
        }

        public async Task<GetManyResult<Contact>> GetAllAsync()
        {
            GetManyResult<Contact> result = await _contactDataAccess.GetAllAsync();
            return result;
        }

        public async Task<GetOneResult<Contact>> GetAsync(string id)
        {
            GetOneResult<Contact> result = await _contactDataAccess.GetByIdAsync(id);
            return result;
        }
    }
}
