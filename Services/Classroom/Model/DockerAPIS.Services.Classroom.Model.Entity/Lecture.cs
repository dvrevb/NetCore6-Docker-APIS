using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerAPIS.Services.Classroom.Model.Entity
{
    public class Lecture
    {
        public string Name { get; set; }
        public List<string> Students { get; set; }
    }
}
