using DockerAPIS.Entities;using DockerAPIS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DockerAPIS.Business.Abstract
{
    public interface IClassroomService
    {
        Task<GetManyResult<Lecture>> GetAllAsync();
        Task<GetOneResult<Lecture>> GetAsync(string id);
        Task<GetOneResult<Lecture>> CreateAsync(string id, Lecture lecture);
    }
}
