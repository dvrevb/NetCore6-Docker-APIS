using Classroom.Entities;

namespace Classroom.DTO
{
    public class LectureDto
    {
        public string? LectureName { get; set; }
        public List<Student>? Students { get; set; }
    }
}
