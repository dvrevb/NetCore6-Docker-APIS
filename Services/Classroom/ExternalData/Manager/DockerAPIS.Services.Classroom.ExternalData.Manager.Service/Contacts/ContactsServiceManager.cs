using DockerAPIS.Services.Classroom.ExternalData.Manager.Interfaces;
using DockerAPIS.Services.Classroom.ExternalData.Model.Contacts.GetContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Services.Classroom.ExternalData.Manager.Service.Contacts
{
    public class ContactsServiceManager : IContactDataStore
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ContactsServiceManager(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<GetContactResponseModel> GetContact(string id)
        {
            var client = httpClientFactory.CreateClient();
            HttpResponseMessage contactHttpResponse = await client.GetAsync("http://contactsapi/api/Contacts/" + id);
            
            if (!contactHttpResponse.IsSuccessStatusCode)
                return null;

            var contact = await contactHttpResponse.Content.ReadFromJsonAsync<GetContactResponseModel>();
            return contact;
        }
    }
}
