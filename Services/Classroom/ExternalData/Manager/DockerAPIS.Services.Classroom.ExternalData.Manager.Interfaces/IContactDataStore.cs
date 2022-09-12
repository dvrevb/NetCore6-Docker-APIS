using DockerAPIS.Services.Classroom.ExternalData.Model.Contacts.GetContact;

namespace DockerAPIS.Services.Classroom.ExternalData.Manager.Interfaces
{
    public interface IContactDataStore
    {
        Task<GetContactResponseModel> GetContact(string id);
    }
}
