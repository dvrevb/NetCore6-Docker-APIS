using AutoMapper;
using DockerAPIS.Services.Contact.Model.Entity;
using DockerAPIS.Services.Contact.Model.Exchange.AddContact;
using DockerAPIS.Services.Contact.Model.Exchange.GetContact;
using DockerAPIS.Services.Contact.Model.Exchange.GetContactsList;

namespace DockerAPIS.Services.Contact.Manager.Mapper
{
    public class ContactMappingProfile : Profile 
    {
        public ContactMappingProfile()
        {
            CreateMap<AddContactRequestModel, Person>();
            CreateMap<Person, AddContactResponseModel>();
            CreateMap<Person, GetContactResponseModel>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year ));
            CreateMap<Person, GetContactsListModel>();
        }
    }
}
