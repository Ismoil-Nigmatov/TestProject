using TestProject.ViewModels;

namespace TestProject.Repository
{
    public interface ITaskRepository
    {
        Task<List<TestProject.Entity.Task>> GetAll();
        Task<TestProject.Entity.Task> Get(int id);
        Task Add(TaskViewModel testViewModel, string userId, string username);
        Task Update(int id, TaskViewModel testViewModel, string userId, string username);
        Task Delete(int id, string userId, string username);
    }
}
