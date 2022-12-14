using AutoMapper;
using DockerAPIS.Services.Contact.Manager.Business.Interface;
using DockerAPIS.Services.Contact.Manager.Operation.Interfaces;
using DockerAPIS.Services.Contact.Model.Entity;
using DockerAPIS.Services.Contact.Model.Exchange.AddContact;
using DockerAPIS.Services.Contact.Model.Exchange.GetContact;
using DockerAPIS.Services.Contact.Model.Exchange.GetContactsList;
using DockerAPIS.Architecture.AppException.Model.Derived.DataNotFound;

namespace DockerAPIS.Services.Contact.Manager.Business.Implementation
{
    public class ContactBusinessManager : IContactBusinessManager
    {
        private readonly IMapper mapper;
        private readonly IContactOperation contactCacheOperations;

        public ContactBusinessManager(IMapper mapper, IContactOperation contactCacheOperations)
        {
            this.mapper = mapper;
            this.contactCacheOperations = contactCacheOperations;
        }
        public async Task<AddContactResponseModel> Create(AddContactRequestModel model)
        {
            string key = Guid.NewGuid().ToString();
            Person contact = mapper.Map<Person>(model);
            await contactCacheOperations.Set(key, contact);
            var responseModel = mapper.Map<AddContactResponseModel>(contact);
            responseModel.Id = key;
            return responseModel;
        }

        public async Task<GetContactResponseModel> Get(string id)
        {
            Person person = await contactCacheOperations.GetByKey(id);
            if(person == null) throw new DataNotFoundException(entityName: "Person", value:id);

            return mapper.Map<GetContactResponseModel>(person);
        }

        public async Task<IEnumerable<GetContactsListModel>> GetAll()
        {
            return mapper.Map<IEnumerable<GetContactsListModel>>(await contactCacheOperations.GetAll());
        }
    }
}
