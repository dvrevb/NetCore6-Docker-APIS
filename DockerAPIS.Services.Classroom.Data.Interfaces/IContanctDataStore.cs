namespace DockerAPIS.Services.Classroom.Data.Interfaces
{
    public interface IContactDataStore
    {
        Contact GetContact(string ID);
    }
}