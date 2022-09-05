using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DockerAPIS.Core.Caching.Abstract;
using DockerAPIS.Entities;

namespace DockerAPIS.DataAccess.Abstract
{
    public interface IClassroomDataAccess : ICaching<Lecture>
    {

    }
}
