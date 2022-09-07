﻿
using Classroom.Services.Abstract;
using DockerAPIs.Services.Classroom.Model.Exchange.Classroom.Add;
using DockerAPIS.Business.Abstract;
using DockerAPIS.Core.Models;
using DockerAPIS.Entities;
using DockerAPIS.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

namespace Classroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;


        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpPut("{lectureId}/{personId}")]
        public async Task<IActionResult> AddStudent(string lectureId, string personId)
        {
            if (String.IsNullOrWhiteSpace(lectureId) || personId == null)
                return BadRequest();

           var result = await _classroomService.AddStudent(lectureId, personId);
            return (result.Success == true) ? Ok(result.Entity) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
                return NotFound();
            
            var result = await _classroomService.GetAsync(id);
            return (result.Success !=false ) ? Ok(result.Entity) : NotFound();
        }

        [HttpPost()]
        public async Task<IActionResult> Add(AddClassroomRequestModel request)
        {
            var id = Guid.NewGuid().ToString();           
            var result = await _classroomService.CreateAsync(id, name);          
            return result.Success==true ?  Ok(id) : BadRequest();
        }
    }
}
