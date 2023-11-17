using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.ViewModels;
using Task = TestProject.Entity.Task;

namespace TestProject.Repository.Impl
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Task>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> Get(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            return task ?? throw new BadHttpRequestException("Task not found.");
        }

        public async System.Threading.Tasks.Task Add(TaskViewModel taskViewModel, string userId, string username)
        {
            Task task = new Task
            {
                Title = taskViewModel.Title,
                Description = taskViewModel.Description,
                Status = taskViewModel.Status,
                DueDate = DateTime.SpecifyKind(taskViewModel.DueDate, DateTimeKind.Utc)
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(userId, username);
        }

        public async System.Threading.Tasks.Task Update(int id, TaskViewModel taskViewModel, string userId, string username)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);

            if (task is null)
            {
                throw new BadHttpRequestException("Task not found");
            }

            task.Title = taskViewModel.Title;
            task.Description = taskViewModel.Description;
            task.Status = taskViewModel.Status;
            task.DueDate = DateTime.SpecifyKind(taskViewModel.DueDate, DateTimeKind.Utc);

            _context.Entry(task).State = EntityState.Modified;

            await _context.SaveChangesAsync(userId, username);
        }

        public async System.Threading.Tasks.Task Delete(int id, string userId, string username)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            if (task is not null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync(userId, username);
            }
        }
    }
}
