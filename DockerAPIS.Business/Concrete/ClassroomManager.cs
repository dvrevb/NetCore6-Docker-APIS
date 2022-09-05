
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.DataAccess.Abstract;
using DockerAPIS.DataAccess.Concrete;
using DockerAPIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Business.Concrete
{
    public class ClassroomManager : IClassroomService
    {
        private readonly IClassroomDataAccess _classroomDataAccess;

        public ClassroomManager(IClassroomDataAccess classroomDataAccess)
        {
            _classroomDataAccess = classroomDataAccess;
        }
        public async Task<GetOneResult<Lecture>> CreateAsync(string id, Lecture lecture)
        {
            GetOneResult<Lecture> result = await _classroomDataAccess.InsertOneAsync(id, lecture);
            return result;
        }

        public async Task<GetManyResult<Lecture>> GetAllAsync()
        {
            GetManyResult<Lecture> result = await _classroomDataAccess.GetAllAsync();
            return result;
        }

        public async Task<GetOneResult<Lecture>> GetAsync(string id)
        {
            GetOneResult<Lecture> result = await _classroomDataAccess.GetByIdAsync(id);
            return result;
        }
    }
}
