using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Entities.DTO
{
    public class LectureDto
    {
        public string? LectureName { get; set; }
        public List<Student>? Students { get; set; }
    }
}
