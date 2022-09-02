namespace Classroom.Entities
{
    public class Lecture
    {
        public string Name { get; set; }
        public List<string> Students { get; set; }

        public Lecture()
        {
            Students = new List<string>();
            Name = "";
        }
    }
}
