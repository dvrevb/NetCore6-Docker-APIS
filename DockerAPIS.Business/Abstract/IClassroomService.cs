using DockerAPIS.Entities;using DockerAPIS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DockerAPIS.Entities.DTO;

namespace DockerAPIS.Business.Abstract
{
    public interface IClassroomService
    {
        Task<GetManyResult<Lecture>> GetAllAsync();
        Task<GetOneResult<LectureDto>> GetAsync(string id);
        Task<GetOneResult<Lecture>> CreateAsync(string id, string name);
        Task<GetOneResult<Lecture>> AddStudent(string lectureId, string studentId);
    }
}
