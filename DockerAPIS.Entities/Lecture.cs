namespace DockerAPIS.Entities
{
    public class Lecture
    {
        public string Name { get; set; }
        public List<string> Students { get; set; }

        public Lecture(string name)
        {
            Name = name;
            Students = new List<string>();
        }

        public Lecture()
        {

        }
    }
}
