using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Services.Contact.Model.Exchange.AddContact
{
    public class AddContactRequestModel
    {
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
