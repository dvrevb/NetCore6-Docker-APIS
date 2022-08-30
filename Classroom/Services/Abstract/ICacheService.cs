namespace Classroom.Services.Abstract
{
    public interface ICacheService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
         IEnumerable<string> GetAllAsync();
    }
}
