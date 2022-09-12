using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Services.Classroom.Model.Exchange.AddStudent
{
    public class AddStudentRequestModel
    {
        public string LectureId { get; set; }
        public string StudentId { get; set; }
    }
}
