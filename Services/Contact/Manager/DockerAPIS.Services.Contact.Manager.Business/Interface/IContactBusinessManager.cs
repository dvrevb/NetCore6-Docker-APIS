using DockerAPIS.Services.Contact.Model.Exchange.AddContact;
using DockerAPIS.Services.Contact.Model.Exchange.GetContact;
using DockerAPIS.Services.Contact.Model.Exchange.GetContactsList;

namespace DockerAPIS.Services.Contact.Manager.Business.Interface
{
    public interface IContactBusinessManager
    {
        Task<IEnumerable<GetContactsListModel>> GetAll();
        Task<GetContactResponseModel> Get(string id);
        Task<AddContactResponseModel> Create(AddContactRequestModel model);
    }
}
