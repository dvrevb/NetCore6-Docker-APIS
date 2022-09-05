
using DockerAPIS.Core.Caching.Abstract;
using DockerAPIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.DataAccess.Abstract
{
    public interface IContactDataAccess : ICaching<Contact>
    {
    }
}
